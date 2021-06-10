using System;
using System.Threading;
using System.Threading.Tasks;

namespace Totalview.Mediators
{
    internal class NotificationSubscription<T> : INotificationSubscription where T : INotification
    {
        /* ----------------------------------------------------------------------------  */
        /*                                  PROPERTIES                                   */
        /* ----------------------------------------------------------------------------  */
        private bool _disposed = false;

        public Guid Id { get; }

        public Type Type { get; }

        public Func<T, CancellationToken, Task> Handler { get; }

        private readonly IMediator _mediator;

        /* ----------------------------------------------------------------------------  */
        /*                                 CONSTRUCTORS                                  */
        /* ----------------------------------------------------------------------------  */
        public NotificationSubscription(Type type, Func<T, CancellationToken, Task> handler, IMediator mediator)
        {
            Id = Guid.NewGuid();
            Type = type;
            Handler = handler;
            _mediator = mediator;
        }

        /* ----------------------------------------------------------------------------  */
        /*                                PUBLIC METHODS                                 */
        /* ----------------------------------------------------------------------------  */
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /* ----------------------------------------------------------------------------  */
        /*                               PROTECTED METHODS                               */
        /* ----------------------------------------------------------------------------  */
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _mediator.Unsubscribe(this);
            }

            _disposed = true;
        }
    }
}
