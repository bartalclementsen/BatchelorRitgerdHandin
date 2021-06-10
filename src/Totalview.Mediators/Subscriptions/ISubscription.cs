using System;

namespace Totalview.Mediators
{
    public interface ISubscription : IDisposable
    {
        public Guid Id { get; }
    }
}
