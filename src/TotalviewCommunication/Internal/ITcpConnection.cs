using System;
using System.Threading;
using System.Threading.Tasks;

namespace Totalview.Communication
{
    internal interface ITcpConnection
    {
        event EventHandler<EventArgs> Disconnected;

        Task ConnectAsync(CancellationToken cancellationToken = default);

        void Disconnect();

        void SendData(int dataSize, byte[] data);
    }
}
