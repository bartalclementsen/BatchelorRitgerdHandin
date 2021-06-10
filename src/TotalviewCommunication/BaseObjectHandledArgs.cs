using Proxy.Streaming;
using System;

namespace Totalview.Communication
{
    internal class BaseObjectHandledArgs : EventArgs
    {
        public T5StreamBaseObject StreamBaseObject { get; private set; }

        public BaseObjectHandledArgs(T5StreamBaseObject streamBaseObject)
        {
            StreamBaseObject = streamBaseObject ?? throw new ArgumentNullException(nameof(streamBaseObject));
        }
    }
}
