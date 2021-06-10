using Grpc.Core;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Totalview.Server;

namespace Totalview.BlazorClient.Client.TotalviewCommunication
{
    public interface ITotalviewCommunicationService
    {
        Task ConnectAsync(string address);

        Task<SetCurrentStateResponse> SetCurrentState(SetCurrentStateRequest setCurrentStateRequest);
    }

    public class TotalviewCommunicationService : ITotalviewCommunicationService
    {
        /* ----------------------------------------------------------------------------  */
        /*                                  PROPERTIES                                   */
        /* ----------------------------------------------------------------------------  */
        private GrpcChannel? _channel;
        private TotalviewService.TotalviewServiceClient? _client;

        private readonly IEnumerable<ITotalviewEventHandler> _totalviewEventHandlers;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly ILogger<TotalviewCommunicationService> _logger;
        private readonly IAccessTokenProvider _accessTokenProvider;

        /* ----------------------------------------------------------------------------  */
        /*                                 CONSTRUCTORS                                  */
        /* ----------------------------------------------------------------------------  */
        public TotalviewCommunicationService(ILogger<TotalviewCommunicationService> logger, IEnumerable<ITotalviewEventHandler> totalviewEventHandlers, IAccessTokenProvider accessTokenProvider)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _totalviewEventHandlers = totalviewEventHandlers ?? throw new ArgumentNullException(nameof(totalviewEventHandlers));
            _accessTokenProvider = accessTokenProvider ?? throw new ArgumentNullException(nameof(accessTokenProvider));

            _cancellationTokenSource = new CancellationTokenSource();

        }

        public Task ConnectAsync(string address)
        {
            _logger.LogInformation($"Connecting to address {address}");

            var credentials = CallCredentials.FromInterceptor(async (context, metadata) =>
            {
                AccessTokenResult? accessTokenResult = await _accessTokenProvider.RequestAccessToken();
                accessTokenResult.TryGetToken(out AccessToken accessToken);
                if (!string.IsNullOrEmpty(accessToken.Value))
                {
                    string token = accessToken.Value;
                    Console.WriteLine(token);

                    metadata.Add("Authorization", $"Bearer {token}");
                }
            });

            var handler = new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler());
            _channel = GrpcChannel.ForAddress(address, new GrpcChannelOptions
            {
                HttpClient = new HttpClient(handler),
                Credentials = ChannelCredentials.Create(new SslCredentials(), credentials)
            });
            _client = new TotalviewService.TotalviewServiceClient(_channel);

            _ = StartListentingAsync();

            return Task.CompletedTask;
        }

        public async Task<SetCurrentStateResponse> SetCurrentState(SetCurrentStateRequest setCurrentStateRequest)
        {
            return await _client!.SetCurrentStateAsync(new SetCurrentStateRequest(setCurrentStateRequest));
        }

        /* ----------------------------------------------------------------------------  */
        /*                                PRIVATE METHODS                                */
        /* ----------------------------------------------------------------------------  */
        private async Task StartListentingAsync()
        {
            if (_client == null)
            {
                throw new Exception($"Can not start listening if {nameof(_client)} is null");
            }

            _logger.LogInformation($"Starting listenting");

            while (_cancellationTokenSource.Token.IsCancellationRequested == false)
            {
                try
                {
                    _logger.LogInformation("Subscribing");
                    AsyncServerStreamingCall<TotalviewEvent> stream = _client.Subscribe(new SubscribeRequest(), cancellationToken: _cancellationTokenSource.Token);

                    _logger.LogInformation("stream created");

                    await foreach (TotalviewEvent response in stream.ResponseStream.ReadAllAsync(_cancellationTokenSource.Token))
                    {
                        _logger.LogInformation("Handeling message");
                        await HandleTotalviewEventAsync(response);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Could not start listening");
                }

                _logger.LogInformation("Will retry in 1000 ms");
                await Task.Delay(1000, _cancellationTokenSource.Token);
            }
        }

        private async Task HandleTotalviewEventAsync(TotalviewEvent totalviewEvent)
        {
            if (totalviewEvent == null)
                return;

            _logger.LogInformation("Handeling Totalview Event");

            bool wasHandled = false;
            foreach (var totalviewEventHandler in _totalviewEventHandlers)
            {
                if (await totalviewEventHandler.HandleTotalviewEventAsync(totalviewEvent))
                {
                    wasHandled = true;
                }
            }

            if (wasHandled == false)
            {
                _logger.LogWarning($"Did not handle event {totalviewEvent}");
            }
        }
    }

}
