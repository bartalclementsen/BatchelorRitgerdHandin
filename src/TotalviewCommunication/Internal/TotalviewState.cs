using Microsoft.Extensions.Logging;
using Proxy.Streaming;
using System.IO;

namespace Totalview.Communication
{
    internal class TotalviewState : ITotalviewState
    {
        /* ----------------------------------------------------------------------------  */
        /*                                  PROPERTIES                                   */
        /* ----------------------------------------------------------------------------  */
        public ITotalviewBasicData? TotalviewBasicData { get; set; }

        public ITcpConnection? ActiveConnection { get; private set; }

        private readonly ILogger? _logger;

        /* ----------------------------------------------------------------------------  */
        /*                                 CONSTRUCTORS                                  */
        /* ----------------------------------------------------------------------------  */
        public TotalviewState(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory?.CreateLogger<TotalviewState>();
        }

        /* ----------------------------------------------------------------------------  */
        /*                                PUBLIC METHODS                                 */
        /* ----------------------------------------------------------------------------  */
        public void EvConnectionStateChanged(ITcpConnection connection, ConnectionState connectionState)
        {
            if (connectionState == ConnectionState.ConnnectionReady)
            {
                ActiveConnection = connection;

                T5StreamConnectionStartup startup = new T5StreamConnectionStartup
                {
                    ClientDescription = "Not a console test application",
                    ClientVersion = "1.0",
                    ConnectionType = ClientTypeEnum.SmartClient,
                    MachineName = "Tobedetermined",
                    NetVersion = 20160501
                };
                SendObject(connection, startup);
            }
        }

        public void SendObject(T5StreamBaseObject baseObject)
        {
            if (ActiveConnection != null)
                SendObject(ActiveConnection, baseObject);
            else
                _logger?.LogWarning("Trying to send data without an active connection");
        }

        public void EvPacketAvailable(ITcpConnection connection, int datasize, byte[] data)
        {
            MemoryStream ms = new ();
            ms.Write(data, 0, datasize);
            ms.Position = 0;

            T5StreamBaseObject baseObject = ProxyStream.CreateFromStream(ms, "");
            _logger?.LogInformation($"recieved packet of type {baseObject.GetType().Name}");

            TotalviewBasicData?.HandleBaseObject(connection, baseObject);
        }

        /* ----------------------------------------------------------------------------  */
        /*                                PRIVATE METHODS                                */
        /* ----------------------------------------------------------------------------  */
        private void SendObject(ITcpConnection connection, T5StreamBaseObject baseObject)
        {
            MemoryStream ms = new ();
            baseObject.StreamOut(ms, "Object");
            int dataSize = (int)ms.Position;
            byte[] data = new byte[dataSize];
            ms.Position = 0;
            ms.Read(data, 0, dataSize);
            connection.SendData(dataSize, data);
        }
    }
}
