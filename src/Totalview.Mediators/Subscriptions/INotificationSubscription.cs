using System;

namespace Totalview.Mediators
{
    internal interface INotificationSubscription : ISubscription
    {
        Type Type { get; }
    }
}
