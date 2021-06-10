using System;
using Totalview.Mediators;
using Totalview.Server.TotalviewSubscriptionsHandlers;

namespace Totalview.Server.Notifications
{
    internal class SubscriptionAddedNotification : INotification
    {
        public ITotalviewSubscription TotalviewSubscription { get; }

        public SubscriptionAddedNotification(ITotalviewSubscription totalviewSubscription)
        {
            TotalviewSubscription = totalviewSubscription ?? throw new ArgumentNullException(nameof(totalviewSubscription));
        }
    }
}
