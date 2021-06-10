using Grpc.Core;
using Grpc.Net.Client;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Totalview.Server;

namespace Totalview.Testers.ServerStressTester
{
    public class ClientViewModel : ViewModelBase, IDisposable
    {
        /* ----------------------------------------------------------------------------  */
        /*                                  PROPERTIES                                   */
        /* ----------------------------------------------------------------------------  */
        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private int _messagesRecieved;
        public int MessagesRecieved
        {
            get => _messagesRecieved;
            set => SetProperty(ref _messagesRecieved, value);
        }

        private DateTime _lastMessageRecievedAt;
        public DateTime LastMessageRecievedAt
        {
            get => _lastMessageRecievedAt;
            set => SetProperty(ref _lastMessageRecievedAt, value);
        }

        private string _lastErrorMsg;
        public string LastErrorMsg
        {
            get => _lastErrorMsg;
            set => SetProperty(ref _lastErrorMsg, value);
        }

        private int _currentTry = 0;
        private int _maxTries = 5;

        private bool disposedValue;
        private GrpcChannel _channel;
        private TotalviewService.TotalviewServiceClient _client;
        private CancellationTokenSource _cancellationTokenSource;
        private Random _random;

        /* ----------------------------------------------------------------------------  */
        /*                                 CONSTRUCTORS                                  */
        /* ----------------------------------------------------------------------------  */
        public ClientViewModel(Random random)
        {
            _random = random;
            _cancellationTokenSource = new CancellationTokenSource();
        }

        /* ----------------------------------------------------------------------------  */
        /*                                PUBLIC METHODS                                 */
        /* ----------------------------------------------------------------------------  */
        public void StartConnecting(string address)
        {
            _channel = GrpcChannel.ForAddress(address, new GrpcChannelOptions
            {
                HttpHandler = new SocketsHttpHandler
                {
                    EnableMultipleHttp2Connections = true
                }
            });
            _client = new TotalviewService.TotalviewServiceClient(_channel);
        }

        public async void Start()
        {
            try
            {
                AsyncServerStreamingCall<TotalviewEvent> stream = _client.Subscribe(new SubscribeRequest(), cancellationToken: _cancellationTokenSource.Token);
                await foreach (var response in stream.ResponseStream.ReadAllAsync(_cancellationTokenSource.Token))
                {
                    MessagesRecieved++;
                    LastMessageRecievedAt = DateTime.Now;
                    LastErrorMsg = null;
                }
            }
            catch (Exception ex)
            {
                LastErrorMsg = ex.Message;
            }

            _currentTry++;
            if (_currentTry < _maxTries)
            {
                await Task.Delay(_random.Next(10, 100));
                Start(); //Try again
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
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
                    _cancellationTokenSource.Dispose();
                    _channel.Dispose();
                }

                _cancellationTokenSource = null;
                _channel = null;
                _client = null;

                disposedValue = true;
            }
        }
    }
}
