using Microsoft.Extensions.Logging;
using Proxy.Streaming;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Totalview.Communication
{
    internal class TotalviewClient : ITotalviewClient
    {
        /* ----------------------------------------------------------------------------  */
        /*                                  PROPERTIES                                   */
        /* ----------------------------------------------------------------------------  */
        public event EventHandler<PackedRecievedArgs>? PackedRecieved;
        public event EventHandler<EventArgs>? Disconnected;

        private readonly ILogger<TotalviewClient>? _logger;
        private readonly ITcpConnection _tcpConnection;
        private readonly ITotalviewState _totalviewState;
        private readonly ITotalviewBasicData _totalviewBasicData;

        /* ----------------------------------------------------------------------------  */
        /*                                 CONSTRUCTORS                                  */
        /* ----------------------------------------------------------------------------  */
        public TotalviewClient(
            ILoggerFactory loggerFactory,
            ITcpConnection tcpConnection,
            ITotalviewState totalviewState, 
            ITotalviewBasicData totalviewBasicData)
        {
            _logger = loggerFactory?.CreateLogger<TotalviewClient>();

            _tcpConnection = tcpConnection ?? throw new ArgumentNullException(nameof(tcpConnection));
            _totalviewState = totalviewState ?? throw new ArgumentNullException(nameof(totalviewState));
            _totalviewBasicData = totalviewBasicData ?? throw new ArgumentNullException(nameof(totalviewBasicData));

            _totalviewBasicData.BaseObjectHandled += TotalviewBasicData_BaseObjectHandled;
            _tcpConnection.Disconnected += TcpConnection_Disconnected;
        }

        /* ----------------------------------------------------------------------------  */
        /*                                PUBLIC METHODS                                 */
        /* ----------------------------------------------------------------------------  */
        public Task ConnectAsync(CancellationToken cancellationToken = default)
        {
            _logger?.LogInformation("Connecting");

            return _tcpConnection.ConnectAsync(cancellationToken);
        }

        public void SendMessage(T5StreamBaseObject baseObject)
        {
            _logger?.LogDebug($"Sending message {baseObject.GetType()}");

            _totalviewState.SendObject(baseObject);
        }

        /* ----------------------------------------------------------------------------  */
        /*                                PRIVATE METHODS                                */
        /* ----------------------------------------------------------------------------  */
        private void TotalviewBasicData_BaseObjectHandled(object? sender, BaseObjectHandledArgs e)
        {
            _logger?.LogDebug($"Triggering {nameof(PackedRecieved)} event");

            PackedRecieved?.Invoke(this, new PackedRecievedArgs(e.StreamBaseObject)); ;
        }

        private void TcpConnection_Disconnected(object? sender, EventArgs e)
        {
            _logger?.LogDebug($"Triggering {nameof(Disconnected)} event");

            Disconnected?.Invoke(this, new EventArgs());
        }
    }
}
