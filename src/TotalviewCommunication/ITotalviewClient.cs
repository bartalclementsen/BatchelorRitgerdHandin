using Proxy.Streaming;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Totalview.Communication
{
    public interface ITotalviewClient
    {
        event EventHandler<PackedRecievedArgs> PackedRecieved;

        event EventHandler<EventArgs> Disconnected;

        Task ConnectAsync(CancellationToken cancellationToken = default);

        void SendMessage(T5StreamBaseObject baseObject);
    }
}
