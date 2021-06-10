using AutoMapper;
using Microsoft.Extensions.Logging;
using Proxy.Streaming;
using System;
using System.Threading;
using System.Threading.Tasks;
using Totalview.Mediators;
using Totalview.Server.Notifications;
using Totalview.Server.TotalviewClassicServerCommunication;

namespace Totalview.Server.RequestHandlers
{
    internal class SetCurrentStateRequestHandler : RequestHandlerBase<Requests.SetCurrentStateRequest, SetCurrentStateResponse>
    {
        /* ----------------------------------------------------------------------------  */
        /*                                  PROPERTIES                                   */
        /* ----------------------------------------------------------------------------  */
        private readonly ILogger<SetCurrentStateRequestHandler> _logger;
        private readonly IMediator _mediator;
        private readonly ITotalviewClassicServerCommunicationService _totalviewClassicServerCommunicationService;
        private readonly IMapper _mapper;

        private TaskCompletionSource<SetCurrentStateResponse>? _taskCompletionSource;
        private CancellationTokenSource? _cancellationTokenSource;
        private ISubscription _notificationSubscription;

        /* ----------------------------------------------------------------------------  */
        /*                                 CONSTRUCTORS                                  */
        /* ----------------------------------------------------------------------------  */
        public SetCurrentStateRequestHandler(
            ILogger<SetCurrentStateRequestHandler> logger,
            IMediator mediator,
            ITotalviewClassicServerCommunicationService totalviewClassicServerCommunicationService,
            IMapper mapper)
            : base(mediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _totalviewClassicServerCommunicationService = totalviewClassicServerCommunicationService ?? throw new ArgumentNullException(nameof(TotalviewClassicServerCommunication));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

            // Register for the Request Result from the server
            _notificationSubscription = _mediator.Subscribe((StreamReservationRecievedNotification request, CancellationToken cancellationToken) =>
            {
                if (_taskCompletionSource != null)
                {
                    _logger.LogInformation("Response from Server recieved");

                    // Map to SetCurrentStateResponse
                    RequestStatus requestStatus = request.StreamReservation.MessageResult switch
                    {
                        T5MessageResultEnum.RequestOK => RequestStatus.Success,
                        T5MessageResultEnum.RequestFail => RequestStatus.Error,
                        _ => RequestStatus.Unknown
                    };

                    string error = request.StreamReservation.MessageError;

                    Reservation reservation = _mapper.Map<Reservation>(request.StreamReservation);

                    _taskCompletionSource?.SetResult(new SetCurrentStateResponse()
                    {
                        Status = requestStatus,
                        Error = error,
                        Reservattion = reservation
                    });

                    _taskCompletionSource = null;
                    _cancellationTokenSource?.Dispose();
                    _cancellationTokenSource = null;

                    _logger.LogInformation($"{nameof(Requests.SetCurrentStateRequest)} handled");
                }

                return Task.CompletedTask;
            });
        }

        /* ----------------------------------------------------------------------------  */
        /*                               PROTECTED METHODS                               */
        /* ----------------------------------------------------------------------------  */
        protected override Task<SetCurrentStateResponse> HandleRequest(Requests.SetCurrentStateRequest request, CancellationToken cancellationToken)
        {
            _taskCompletionSource = new TaskCompletionSource<SetCurrentStateResponse>();
            _cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            _cancellationTokenSource.CancelAfter(10000); // Timeout

            // Map to stream reservation
            var reservation = new T5StreamReservation
            {
                Id = 0,
                Key = null,
                GlobalId = 0,

                ResourceId = request.UserId,
                StartCode = 1,
                Subject = "Subject 1",

                StartTime = DateTime.Now.AddHours(0),
                EndTime = DateTime.Now.AddHours(1),
                EndType = Proxy.Streaming.EndTypeEnum.Expected,

                Location = "Some location",

                OriginExecution = Proxy.Streaming.OriginExecutionEnum.SetCurrent,
                AppointmentType = Proxy.Streaming.AppointmentTypeEnum.Normal,
                Attachment = null,
                Details = new T5StreamReservationDetail(),
                History = false,
                MadeFrom = Proxy.Streaming.ClientTypeEnum.Client,
                MessageType = T5MessageTypeEnum.Create,
                ReservationType = Proxy.Streaming.ReservationTypeEnum.Current,
                Scope = Proxy.Streaming.ScopeTypeEnum.Public,
                ServiceCode = null
            };

            _totalviewClassicServerCommunicationService.SendMessage(reservation);

            return _taskCompletionSource.Task;
        }
    }
}
