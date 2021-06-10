using System;
using System.Threading;
using System.Threading.Tasks;

namespace Totalview.Mediators
{
    public interface IMediator
    {
        ISubscription Subscribe<T>(Func<T, CancellationToken, Task> handler) where T : INotification;

        Task SendNotificationAsync<T>(T notification, CancellationToken cancellationToken = default) where T : INotification;

        ISubscription Subscribe<T, K>(Func<T, CancellationToken, Task<K>> handler) where T : class, IRequest<K>;

        Task<T> RequestAsync<T>(IRequest<T> request, CancellationToken cancellationToken = default);

        void Unsubscribe(ISubscription subscription);
    }
}
