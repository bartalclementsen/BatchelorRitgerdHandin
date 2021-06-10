using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Totalview.Mediators;
using Totalview.Server.Notifications;

namespace Totalview.Server.TotalviewSubscriptionsHandlers
{
    internal class TotalviewSubscriptionsHandler : ITotalviewSubscriptionsHandler
    {
        /* ----------------------------------------------------------------------------  */
        /*                                PUBLIC METHODS                                 */
        /* ----------------------------------------------------------------------------  */
        public List<ITotalviewSubscription> _subscriptions = new();

        private readonly ILogger<TotalviewSubscriptionsHandler> _logger;
        private readonly IMediator _mediator;
        private readonly ITotalviewSubscriptionFactory _totalviewSubscriptionFactory;

        /* ----------------------------------------------------------------------------  */
        /*                                 CONSTRUCTORS                                  */
        /* ----------------------------------------------------------------------------  */
        public TotalviewSubscriptionsHandler(ILogger<TotalviewSubscriptionsHandler> logger, IMediator mediator, ITotalviewSubscriptionFactory totalviewSubscriptionFactory)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _totalviewSubscriptionFactory = totalviewSubscriptionFactory ?? throw new ArgumentNullException(nameof(totalviewSubscriptionFactory));
        }

        /* ----------------------------------------------------------------------------  */
        /*                                PUBLIC METHODS                                 */
        /* ----------------------------------------------------------------------------  */
        public async Task AddSubscriptionAsync(SubscribeRequest subscribeRequest, IServerStreamWriter<TotalviewEvent> responseStream, ServerCallContext context)
        {
            _logger.LogInformation("Adding subscription");

            ITotalviewSubscription totalviewSubscription = _totalviewSubscriptionFactory.Create(subscribeRequest, responseStream, context);

            try
            {
                _subscriptions.Add(totalviewSubscription);

                _logger.LogInformation($"Subscription added: {totalviewSubscription}");

                _logger.LogDebug("Sending subscription added notification");

                await _mediator.SendNotificationAsync(new SubscriptionAddedNotification(totalviewSubscription), context.CancellationToken);

                _logger.LogDebug("Subscription added notification sent");

                await totalviewSubscription.WaitWhileConnectedAsync();
            }
            catch (Exception ex)
            {
                if (ex is TaskCanceledException)
                {
                    _logger.LogInformation("Subscription disconnected");
                }
                else
                {
                    _logger.LogError(ex, "Subscription disconnected unexpectedly");
                }
            }
            finally
            {
                await RemoveSubscriptionAsync(totalviewSubscription);
            }
        }

        public void SendToAll(TotalviewEvent totalviewEvent)
        {
            _logger.LogInformation($"Sending Totalview event to any connected subscriptions");

            Parallel.ForEach(_subscriptions, async client =>
            {
                await client.SendTotalviewEvent(totalviewEvent);
            });
        }

        /* ----------------------------------------------------------------------------  */
        /*                                PRIVATE METHODS                                */
        /* ----------------------------------------------------------------------------  */
        private async Task RemoveSubscriptionAsync(ITotalviewSubscription totalviewSubscription)
        {
            _logger.LogInformation($"Removing subscription {totalviewSubscription}");

            try
            {
                if (_subscriptions.Contains(totalviewSubscription) == false)
                {
                    _logger.LogWarning($"Could not remove subscription {totalviewSubscription}. Not found in list.");
                    return;
                }

                _subscriptions.Remove(totalviewSubscription);

                _logger.LogInformation($"Subscription removed: {totalviewSubscription}");

                _logger.LogInformation("Sending subscription removed notification");

                await _mediator.SendNotificationAsync(new SubscriptionRemovedNotification(totalviewSubscription));

                _logger.LogInformation("Subscription removed notification sent");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Could not remove subscription {totalviewSubscription}");
            }
        }
    }
}
