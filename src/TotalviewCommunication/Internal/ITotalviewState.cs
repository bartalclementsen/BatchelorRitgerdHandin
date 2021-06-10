using Proxy.Streaming;

namespace Totalview.Communication
{
    internal interface ITotalviewState
    {
        ITotalviewBasicData? TotalviewBasicData { get; set; }

        ITcpConnection? ActiveConnection { get; }

        void SendObject(T5StreamBaseObject baseObject);

        void EvConnectionStateChanged(ITcpConnection connection, ConnectionState connectionState);

        void EvPacketAvailable(ITcpConnection connection, int datasize, byte[] data);
    }
}
