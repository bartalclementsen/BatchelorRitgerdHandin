using Proxy.Streaming;
using System;

namespace Totalview.Communication
{
    internal interface ITotalviewBasicData
    {
        event EventHandler<BaseObjectHandledArgs> BaseObjectHandled;

        void HandleBaseObject(ITcpConnection connection, T5StreamBaseObject streamBaseObject);
    }
}
