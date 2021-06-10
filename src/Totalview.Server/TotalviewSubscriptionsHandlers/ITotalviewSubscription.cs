using System;
using System.Threading.Tasks;

namespace Totalview.Server.TotalviewSubscriptionsHandlers
{
    internal interface ITotalviewSubscription : IDisposable
    {
        Task WaitWhileConnectedAsync();

        Task SendTotalviewEvent(TotalviewEvent totalviewEvent);
    }
}
