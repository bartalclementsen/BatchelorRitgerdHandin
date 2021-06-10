using Proxy.Streaming;

namespace Totalview.Server.TotalviewClassicServerCommunication.StreamBaseObjectHandlers
{
    internal interface IStreamBaseObjectHandler
    {
        bool HandlePacked(T5StreamBaseObject packet);
    }
}
