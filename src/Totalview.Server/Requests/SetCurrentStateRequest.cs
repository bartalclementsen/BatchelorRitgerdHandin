using Totalview.Mediators;

namespace Totalview.Server.Requests
{
    internal class SetCurrentStateRequest : IRequest<SetCurrentStateResponse>
    {
        public int UserId { get; init; }
    }

}
