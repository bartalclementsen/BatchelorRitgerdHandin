using Proxy.Streaming;
using System;

namespace Totalview.Communication
{
    public class PackedRecievedArgs : EventArgs
    {
        public T5StreamBaseObject StreamBaseObject { get; private set; }

        public PackedRecievedArgs(T5StreamBaseObject streamBaseObject)
        {
            StreamBaseObject = streamBaseObject ?? throw new ArgumentNullException(nameof(streamBaseObject));
        }
    }
}
