using Grpc.Core;
using System.Threading.Tasks;

namespace Totalview.Server.TotalviewSubscriptionsHandlers
{
    internal interface ITotalviewSubscriptionsHandler
    {
        Task AddSubscriptionAsync(SubscribeRequest subscribeRequest, IServerStreamWriter<TotalviewEvent> responseStream, ServerCallContext context);

        void SendToAll(TotalviewEvent totalviewEvent);
    }
}
