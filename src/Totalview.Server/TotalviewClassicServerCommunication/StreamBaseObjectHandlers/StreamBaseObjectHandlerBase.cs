using Proxy.Streaming;

namespace Totalview.Server.TotalviewClassicServerCommunication.StreamBaseObjectHandlers
{
    public abstract class StreamBaseObjectHandlerBase<T> : IStreamBaseObjectHandler where T : T5StreamBaseObject
    {
        public bool HandlePacked(T5StreamBaseObject packet)
        {
            if (packet is T castPacket)
            {
                HandlePacket(castPacket);
                return true;
            }
            return false;
        }

        protected abstract void HandlePacket(T packet);
    }
}
