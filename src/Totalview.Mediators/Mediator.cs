using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Totalview.Mediators
{
    internal class Mediator : IMediator
    {
        /* ----------------------------------------------------------------------------  */
        /*                                  PROPERTIES                                   */
        /* ----------------------------------------------------------------------------  */
        private readonly object _notificationLock = new();
        private readonly object _requestHandlersLock = new();
        private readonly Dictionary<Type, Dictionary<Guid, INotificationSubscription>> _notificationSubscriptions = new();
        private readonly Dictionary<Type, IRequestHandlerSubscription> _requestHandlers = new();

        /* ----------------------------------------------------------------------------  */
        /*                                PUBLIC METHODS                                 */
        /* ----------------------------------------------------------------------------  */
        public ISubscription Subscribe<T>(Func<T, CancellationToken, Task> handler) where T : INotification
        {
            lock (_notificationLock)
            {
                NotificationSubscription<T> subscription = new(typeof(T), handler, this);
                if (!_notificationSubscriptions.TryGetValue(typeof(T), out Dictionary<Guid, INotificationSubscription>? subscriptions))
                {
                    subscriptions = new Dictionary<Guid, INotificationSubscription>();
                    _notificationSubscriptions.Add(typeof(T), subscriptions);
                }

                subscriptions.Add(subscription.Id, subscription);
                return subscription;
            }
        }

        public async Task SendNotificationAsync<T>(T notification, CancellationToken cancellationToken = default) where T : INotification
        {
            Dictionary<Guid, INotificationSubscription>? subscriptions = null;

            lock (_notificationLock)
            {
                if (!_notificationSubscriptions.TryGetValue(notification.GetType(), out subscriptions))
                {
                    return;
                }
            }

            foreach (NotificationSubscription<T> subscription in subscriptions.Values.Cast<NotificationSubscription<T>>())
            {
                await subscription.Handler.Invoke(notification, cancellationToken);
            }
        }

        public ISubscription Subscribe<T, K>(Func<T, CancellationToken, Task<K>> handler) where T : class, IRequest<K>
        {
            lock (_requestHandlersLock)
            {
                if (_requestHandlers.ContainsKey(typeof(T)))
                {
                    throw new RequestAlreadySubscribedException();
                }

                Type requestType = handler.GetType().GenericTypeArguments.First();
                RequestHandlerSubscription<K> requestHandlerSubscription = new(requestType, (s, ct) =>
                {
                    return handler.Invoke((T)s, ct);
                }, this);
                _requestHandlers.Add(requestType, requestHandlerSubscription);
                return requestHandlerSubscription;
            }
        }

        public Task<T> RequestAsync<T>(IRequest<T> request, CancellationToken cancellationToken = default)
        {
            IRequestHandlerSubscription? requestHandlerSubscription = null;

            lock (_requestHandlersLock)
            {
                if (_requestHandlers.TryGetValue(request.GetType(), out requestHandlerSubscription) == false)
                {
                    throw new RequestNotHandledException();
                }
            }

            return ((RequestHandlerSubscription<T>)requestHandlerSubscription).Handler.Invoke(request, cancellationToken);
        }

        public void Unsubscribe(ISubscription subscription)
        {
            if (subscription is INotificationSubscription notificationSubscription)
            {
                lock (_notificationLock)
                {
                    if (_notificationSubscriptions.TryGetValue(notificationSubscription.Type, out Dictionary<Guid, INotificationSubscription>? subscriptions))
                    {
                        subscriptions.Remove(subscription.Id);

                        if (subscriptions.Any() == false)
                        {
                            _notificationSubscriptions.Remove(notificationSubscription.Type);
                        }
                    }
                }
            }
            else if (subscription is IRequestHandlerSubscription requestSubscription)
            {
                lock (_requestHandlersLock)
                {
                    if (_requestHandlers.ContainsKey(requestSubscription.Type))
                    {
                        _requestHandlers.Remove(requestSubscription.Type);
                    }
                }
            }
        }
    }
}
