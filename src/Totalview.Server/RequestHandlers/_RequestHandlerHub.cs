using System.Collections.Generic;

namespace Totalview.Server.RequestHandlers
{
    internal interface IRequestHandlerHub { }

    internal class RequestHandlerHub : IRequestHandlerHub
    {
        private readonly IEnumerable<IRequestHandler> _requestHandlers;

        public RequestHandlerHub(IEnumerable<IRequestHandler> requestHandlers)
        {
            _requestHandlers = requestHandlers;
        }
    }
}
