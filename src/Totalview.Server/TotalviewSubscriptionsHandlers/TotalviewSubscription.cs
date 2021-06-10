using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Totalview.Server.TotalviewSubscriptionsHandlers
{
    internal class TotalviewSubscription : ITotalviewSubscription
    {
        /* ----------------------------------------------------------------------------  */
        /*                                  PROPERTIES                                   */
        /* ----------------------------------------------------------------------------  */
        private TaskCompletionSource _taskCompletionSource = new TaskCompletionSource();
        private bool disposedValue;

        private readonly ILogger<TotalviewSubscription> _logger;
        private readonly SubscribeRequest _subscribeRequest;
        private readonly IServerStreamWriter<TotalviewEvent> _responseStream;
        private readonly ServerCallContext _context;
        private readonly ClaimsPrincipal? _user;
        private readonly string _userName;
        private readonly string _recId;

        /* ----------------------------------------------------------------------------  */
        /*                                 CONSTRUCTORS                                  */
        /* ----------------------------------------------------------------------------  */
        public TotalviewSubscription(ILogger<TotalviewSubscription> logger, SubscribeRequest subscribeRequest, IServerStreamWriter<TotalviewEvent> responseStream, ServerCallContext context)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _subscribeRequest = subscribeRequest ?? throw new ArgumentNullException(nameof(subscribeRequest));
            _responseStream = responseStream ?? throw new ArgumentNullException(nameof(responseStream));
            _context = context ?? throw new ArgumentNullException(nameof(context));

            _user = _context.GetHttpContext().User;

            _userName = _user?.Identity?.Name ?? "Unknown";
            _recId = _user?.FindFirstValue("sub") ?? "";

            context.CancellationToken.Register(() =>
            {
                _taskCompletionSource.TrySetCanceled();
            });
        }

        /* ----------------------------------------------------------------------------  */
        /*                                PUBLIC METHODS                                 */
        /* ----------------------------------------------------------------------------  */
        public Task SendTotalviewEvent(TotalviewEvent totalviewEvent)
        {
            try
            {
                _logger.LogInformation($"Sending Totalview event");
                return _responseStream.WriteAsync(totalviewEvent);
            }
            catch (Exception ex)
            {
                // TODO: Handle this, maybe disconnect client
                _logger.LogError(ex, "Could not send to client");
                return Task.CompletedTask;
            }
        }

        public Task WaitWhileConnectedAsync()
        {
            return _taskCompletionSource.Task;
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public override string ToString()
        {
            return $"{_userName} (Id: {_recId})";
        }

        /* ----------------------------------------------------------------------------  */
        /*                               PROTECTED METHODS                               */
        /* ----------------------------------------------------------------------------  */
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _taskCompletionSource.SetResult();
                }

                disposedValue = true;
            }
        }
    }
}
