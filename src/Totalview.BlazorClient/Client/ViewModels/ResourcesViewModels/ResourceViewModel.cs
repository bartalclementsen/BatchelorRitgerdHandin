using System;
using System.Threading;
using System.Threading.Tasks;
using Totalview.BlazorClient.Client.Messages;
using Totalview.BlazorClient.Client.TotalviewCommunication;
using Totalview.BlazorMvvm;
using Totalview.Mediators;
using Totalview.Server;

namespace Totalview.BlazorClient.Client.ViewModels
{
    public class ResourceViewModel : ViewModelBase
    {
        /* ----------------------------------------------------------------------------  */
        /*                                  PROPERTIES                                   */
        /* ----------------------------------------------------------------------------  */
        private int? _recId;
        public int? RecId
        {
            get => _recId;
            set => SetProperty(ref _recId, value);
        }

        private string? _fullName;
        public string? FullName
        {
            get => _fullName;
            set => SetProperty(ref _fullName, value);
        }

        private string? _identification;
        public string? Identification
        {
            get => _identification;
            set => SetProperty(ref _identification, value);
        }

        private string _currentStateColor = "#000000";
        public string CurrentStateColor
        {
            get => _currentStateColor;
            set => SetProperty(ref _currentStateColor, value);
        }

        private string? _currentState;
        public string? CurrentState
        {
            get => _currentState;
            set => SetProperty(ref _currentState, value);
        }

        private string? _currentStateSubject;
        public string? CurrentStateSubject
        {
            get => _currentStateSubject;
            set => SetProperty(ref _currentStateSubject, value);
        }

        private string? _currentStateStart;
        public string? CurrentStateStart
        {
            get => _currentStateStart;
            set => SetProperty(ref _currentStateStart, value);
        }

        private string? __currentStateEnd;
        public string? CurrentStateEnd
        {
            get => __currentStateEnd;
            set => SetProperty(ref __currentStateEnd, value);
        }

        private int? currentStateId;
        private State? _state;


        private readonly ITotalviewDataReaderCache _totalviewDataReaderCache;
        private readonly IMediator _mediator;

        private readonly ISubscription _reservationUpdatedMessageToken;
        private readonly ISubscription _reservationAddedNotificationToken;
        private readonly ISubscription _resourcesUpdatedMessageToken;

        /* ----------------------------------------------------------------------------  */
        /*                                 CONSTRUCTORS                                  */
        /* ----------------------------------------------------------------------------  */
        public ResourceViewModel(ITotalviewDataReaderCache totalviewDataReaderCache, IMediator mediator)
        {
            _totalviewDataReaderCache = totalviewDataReaderCache ?? throw new ArgumentNullException(nameof(totalviewDataReaderCache));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

            _resourcesUpdatedMessageToken = _mediator.Subscribe((ResourcesUpdatedNotification notification, CancellationToken ct) =>
            {
                if (RecId != null && notification.Resources.ContainsKey(RecId.Value))
                {
                    Update(notification.Resources[RecId.Value]);
                }

                return Task.CompletedTask;
            });

            _reservationAddedNotificationToken = _mediator.Subscribe((ReservationAddedNotification notification, CancellationToken ct) =>
            {
                if (RecId != null && notification.ReservationsByRecourceId.ContainsKey(RecId.Value))
                {
                    Update(notification.ReservationsByRecourceId[RecId.Value]);
                }

                return Task.CompletedTask;
            });

            _reservationUpdatedMessageToken = _mediator.Subscribe((ReservationUpdatedNotification notification, CancellationToken ct) =>
            {
                if (RecId != null && notification.ReservationsByRecourceId.ContainsKey(RecId.Value))
                {
                    Update(notification.ReservationsByRecourceId[RecId.Value]);
                }

                return Task.CompletedTask;
            });

            _reservationUpdatedMessageToken = _mediator.Subscribe((StateUpdatedNotification notification, CancellationToken ct) =>
            {
                if (currentStateId != null && notification.States.ContainsKey(currentStateId.Value))
                {
                    Update(notification.States[currentStateId.Value]);
                }

                return Task.CompletedTask;
            });

            _reservationUpdatedMessageToken = _mediator.Subscribe((StateAddedNotification notification, CancellationToken ct) =>
            {
                if (currentStateId != null && notification.States.ContainsKey(currentStateId.Value))
                {
                    Update(notification.States[currentStateId.Value]);
                }

                return Task.CompletedTask;
            });
        }

        /* ----------------------------------------------------------------------------  */
        /*                                PUBLIC METHODS                                 */
        /* ----------------------------------------------------------------------------  */
        public void Update(Resource resource)
        {
            RecId = resource.RecId;
            FullName = $"{resource.FirstName} {resource.LastName}";
            Identification = resource.UserId;
        }

        public void Update(Reservation reservation)
        {
            currentStateId = reservation.StartCode;

            CurrentStateSubject = reservation?.Subject;
            CurrentStateStart = reservation?.StartTime.ToDateTime().ToString("HH:mm");
            CurrentStateEnd = reservation?.EndTime.ToDateTime().ToString("HH:mm");

            State? foundState = null;
            if (_state?.StateId == reservation?.StartCode)
            {
                foundState = _state;
            }
            else if (reservation?.StartCode != null)
            {
                foundState = _totalviewDataReaderCache.States.Get(reservation.StartCode);
            }

            Update(foundState);
        }

        public void Update(State? state)
        {
            _state = state;
            CurrentStateColor = state?.GetHexColor() ?? "#000000";
            CurrentState = state?.Caption;
        }

        /* ----------------------------------------------------------------------------  */
        /*                               PROTECTED METHODS                               */
        /* ----------------------------------------------------------------------------  */
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _reservationUpdatedMessageToken.Dispose();
                _reservationAddedNotificationToken.Dispose();
                _resourcesUpdatedMessageToken.Dispose();
            }
        }
    }
}
