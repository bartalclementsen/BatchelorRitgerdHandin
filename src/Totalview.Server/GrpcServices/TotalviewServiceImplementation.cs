using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Totalview.Mediators;
using Totalview.Server.Requests;
using Totalview.Server.TotalviewSubscriptionsHandlers;

namespace Totalview.Server.GrpcServices
{
    [Authorize]
    internal class TotalviewServiceImplementation : TotalviewService.TotalviewServiceBase
    {
        /* ----------------------------------------------------------------------------  */
        /*                                  PROPERTIES                                   */
        /* ----------------------------------------------------------------------------  */
        private readonly ILogger<TotalviewServiceImplementation> _logger;
        private readonly ITotalviewSubscriptionsHandler _totalviewSubscriptionsHandler;
        private readonly IMediator _mediator;

        /* ----------------------------------------------------------------------------  */
        /*                                 CONSTRUCTORS                                  */
        /* ----------------------------------------------------------------------------  */
        public TotalviewServiceImplementation(
            ILogger<TotalviewServiceImplementation> logger,
            ITotalviewSubscriptionsHandler totalviewSubscriptionsHandler,
            IMediator mediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _totalviewSubscriptionsHandler = totalviewSubscriptionsHandler ?? throw new ArgumentNullException(nameof(totalviewSubscriptionsHandler));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /* ----------------------------------------------------------------------------  */
        /*                                PUBLIC METHODS                                 */
        /* ----------------------------------------------------------------------------  */
        public override Task Subscribe(SubscribeRequest subscribeRequest, IServerStreamWriter<TotalviewEvent> responseStream, ServerCallContext context)
        {
            _logger.LogDebug(nameof(Subscribe));
            return _totalviewSubscriptionsHandler.AddSubscriptionAsync(subscribeRequest, responseStream, context);
        }


        public override Task<ReservationUserList> GetAllAppointments(Empty request, ServerCallContext context)
        {
            _logger.LogDebug(nameof(GetAllAppointments));
            return _mediator.RequestAsync(new GetAllAppointmentsRequest(), context.CancellationToken);
        }

        public override Task<Empty> GetBasicData(Empty request, ServerCallContext context)
        {
            _logger.LogDebug(nameof(GetBasicData));
            return _mediator.RequestAsync(new GetBasicDataRequest(), context.CancellationToken);
        }

        public override Task<Empty> SetEventsAccepted(SetEventsAcceptedRequest request, ServerCallContext context)
        {
            _logger.LogDebug(nameof(GetBasicData));
            return _mediator.RequestAsync(new Requests.SetEventsAcceptedRequest(), context.CancellationToken);
        }

        public override Task<Empty> GetUserImage(GetUserImageRequest request, ServerCallContext context)
        {
            _logger.LogDebug(nameof(GetUserImage));
            return _mediator.RequestAsync(new Requests.GetUserImageRequest(), context.CancellationToken);
        }

        public override Task<Empty> GetAllUserImages(Empty request, ServerCallContext context)
        {
            _logger.LogDebug(nameof(GetAllUserImages));
            return _mediator.RequestAsync(new Requests.GetAllUserImagesRequest(), context.CancellationToken);
        }

        public override Task<Empty> GetUserNumberLog(GetUserNumberLogRequest request, ServerCallContext context)
        {
            _logger.LogDebug(nameof(GetUserNumberLog));
            return _mediator.RequestAsync(new Requests.GetUserNumberLogRequest(), context.CancellationToken);
        }

        public override Task<Empty> MakeCallFromNumberToNumber(MakeCallFromNumberToNumberRequest request, ServerCallContext context)
        {
            _logger.LogDebug(nameof(MakeCallFromNumberToNumber));
            return _mediator.RequestAsync(new Requests.MakeCallFromNumberToNumberRequest(), context.CancellationToken);
        }

        public override Task<SetCurrentStateResponse> SetCurrentState(SetCurrentStateRequest request, ServerCallContext context)
        {
            _logger.LogDebug(nameof(SetCurrentState));

            string? subject = context.GetHttpContext().User.FindFirstValue(JwtClaimTypes.Subject);
            int userId = int.Parse(subject);

            return _mediator.RequestAsync(new Requests.SetCurrentStateRequest()
            {
                UserId = userId
            }, context.CancellationToken);
        }
    }
}
