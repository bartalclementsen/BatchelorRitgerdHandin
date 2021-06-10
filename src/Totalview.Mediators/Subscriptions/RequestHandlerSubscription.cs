using System;
using System.Threading;
using System.Threading.Tasks;

namespace Totalview.Mediators
{
    internal class RequestHandlerSubscription<T> : IRequestHandlerSubscription
    {
        /* ----------------------------------------------------------------------------  */
        /*                                  PROPERTIES                                   */
        /* ----------------------------------------------------------------------------  */
        private bool _disposed = false;

        public Guid Id { get; }

        public Type Type { get; }

        public Func<IRequest<T>, CancellationToken, Task<T>> Handler { get; }

        private readonly IMediator _mediator;

        /* ----------------------------------------------------------------------------  */
        /*                                 CONSTRUCTORS                                  */
        /* ----------------------------------------------------------------------------  */
        public RequestHandlerSubscription(Type type, Func<IRequest<T>, CancellationToken, Task<T>> handler, IMediator mediator)
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
