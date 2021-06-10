using System;
using Totalview.Mediators;
using Totalview.Server.TotalviewSubscriptionsHandlers;

namespace Totalview.Server.Notifications
{
    internal class SubscriptionRemovedNotification : INotification
    {
        public ITotalviewSubscription TotalviewSubscription { get; }

        public SubscriptionRemovedNotification(ITotalviewSubscription totalviewSubscription)
        {
            TotalviewSubscription = totalviewSubscription ?? throw new ArgumentNullException(nameof(totalviewSubscription));
        }
    }
}
