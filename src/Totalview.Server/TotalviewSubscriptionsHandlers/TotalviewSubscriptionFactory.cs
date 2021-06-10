using Grpc.Core;
using Microsoft.Extensions.Logging;
namespace Totalview.Server.TotalviewSubscriptionsHandlers
{
    internal interface ITotalviewSubscriptionFactory
    {
        ITotalviewSubscription Create(SubscribeRequest subscribeRequest, IServerStreamWriter<TotalviewEvent> responseStream, ServerCallContext context);
    }

    internal class TotalviewSubscriptionFactory : ITotalviewSubscriptionFactory
    {
        /* ----------------------------------------------------------------------------  */
        /*                                  PROPERTIES                                   */
        /* ----------------------------------------------------------------------------  */
        private readonly ILoggerFactory _loggerFactory;

        /* ----------------------------------------------------------------------------  */
        /*                                 CONSTRUCTORS                                  */
        /* ----------------------------------------------------------------------------  */
        public TotalviewSubscriptionFactory(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        /* ----------------------------------------------------------------------------  */
        /*                                PUBLIC METHODS                                 */
        /* ----------------------------------------------------------------------------  */
        public ITotalviewSubscription Create(SubscribeRequest subscribeRequest, IServerStreamWriter<TotalviewEvent> responseStream, ServerCallContext context)
        {
            return new TotalviewSubscription(_loggerFactory.CreateLogger<TotalviewSubscription>(), subscribeRequest, responseStream, context);
        }
    }
}
