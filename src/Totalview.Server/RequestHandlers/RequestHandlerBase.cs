using System.Threading;
using System.Threading.Tasks;
using Totalview.Mediators;

namespace Totalview.Server.RequestHandlers
{
    internal abstract class RequestHandlerBase<T,K> : IRequestHandler where T : class, IRequest<K>
    {
        private readonly ISubscription _subscription;

        public RequestHandlerBase(IMediator mediator)
        {
            // Register as a Request Handler
            _subscription = mediator.Subscribe((T request, CancellationToken cancellationToken) =>
            {
                return HandleRequest(request, cancellationToken);
            });
        }

        protected abstract Task<K> HandleRequest(T request, CancellationToken cancellationToken);
    }
}
