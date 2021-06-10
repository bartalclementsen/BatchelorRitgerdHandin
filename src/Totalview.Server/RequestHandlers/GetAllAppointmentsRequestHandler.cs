using Microsoft.Extensions.Logging;
using Proxy.Streaming;
using System.Threading;
using System.Threading.Tasks;
using Totalview.Client.Communication.Model;
using Totalview.Mediators;
using Totalview.Server.Notifications;
using Totalview.Server.Requests;
using Totalview.Server.TotalviewClassicServerCommunication;

namespace Totalview.Server.RequestHandlers
{
    internal class GetAllAppointmentsRequestHandler : RequestHandlerBase<GetAllAppointmentsRequest, ReservationUserList>
    {
        /* ----------------------------------------------------------------------------  */
        /*                                  PROPERTIES                                   */
        /* ----------------------------------------------------------------------------  */
        private readonly ILogger<GetAllAppointmentsRequestHandler> _logger;
        private readonly IMediator _mediator;
        private readonly ITotalviewClassicServerCommunicationService _totalviewClassicServerCommunicationService;

        private readonly ISubscription _streamReservationUserListRecievedNotificationSubscription;

        private TaskCompletionSource<ReservationUserList>? _taskCompletionSource;
        private CancellationTokenSource? _cancellationTokenSource;

        /* ----------------------------------------------------------------------------  */
        /*                                 CONSTRUCTORS                                  */
        /* ----------------------------------------------------------------------------  */
        public GetAllAppointmentsRequestHandler(
            ILogger<GetAllAppointmentsRequestHandler> logger,
            IMediator mediator,
            ITotalviewClassicServerCommunicationService totalviewClassicServerCommunicationService)
            : base(mediator)
        {
            _logger = logger;
            _mediator = mediator;
            _totalviewClassicServerCommunicationService = totalviewClassicServerCommunicationService;

            // Register for the Request Result from the server
            _streamReservationUserListRecievedNotificationSubscription = _mediator.Subscribe((StreamReservationUserListRecievedNotification request, CancellationToken cancellationToken) =>
            {
                if (_taskCompletionSource != null)
                {
                    _logger.LogInformation("Response from Server recieved");

                    _taskCompletionSource?.SetResult(request.ReservationUserList);
                    _taskCompletionSource = null;
                    _cancellationTokenSource?.Dispose();
                    _cancellationTokenSource = null;

                    _logger.LogInformation($"{nameof(GetAllAppointmentsRequest)} handled");
                }

                return Task.CompletedTask;
            });
        }

        /* ----------------------------------------------------------------------------  */
        /*                                PRIVATE METHODS                                */
        /* ----------------------------------------------------------------------------  */
        protected override async Task<ReservationUserList> HandleRequest(GetAllAppointmentsRequest request, CancellationToken cancellationToken)
        {
            _taskCompletionSource = new TaskCompletionSource<ReservationUserList>();
            _cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            _cancellationTokenSource.CancelAfter(10000); // Timeout

            _cancellationTokenSource.Token.Register(() =>
            {
                _taskCompletionSource?.SetCanceled();
            });

            _logger.LogInformation("Send request to the classic server");

            _totalviewClassicServerCommunicationService.SendMessage(T5StreamProxyCommandFactory.CreateGetAllAppointments());

            _logger.LogInformation("Waiting for response");

            return await _taskCompletionSource.Task;
        }
    }
}
