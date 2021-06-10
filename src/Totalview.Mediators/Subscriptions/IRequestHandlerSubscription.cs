using System;

namespace Totalview.Mediators
{
    internal interface IRequestHandlerSubscription : ISubscription
    {
        Type Type { get; }
    }
}
