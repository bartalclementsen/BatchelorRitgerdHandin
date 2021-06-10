using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Totalview.BlazorClient.Client.Messages;
using Totalview.BlazorClient.Client.Services;
using Totalview.BlazorClient.Client.TotalviewCommunication;
using Totalview.BlazorMvvm;
using Totalview.Mediators;
using Totalview.Server;

namespace Totalview.BlazorClient.Client.ViewModels
{
    public class HeaderViewModel : ViewModelBase
    {
        /* ----------------------------------------------------------------------------  */
        /*                                  PROPERTIES                                   */
        /* ----------------------------------------------------------------------------  */
        private int? _recId;
        private int? _currentStateId;

        private string? _fullName;
        public string? FullName
        {
            get => _fullName;
            set => SetProperty(ref _fullName, value);
        }

        private string? _currentStateText;
        public string? CurrentStateText
        {
            get => _currentStateText;
            set => SetProperty(ref _currentStateText, value);
        }

        private string _currentStateColor = "#000000";
        public string CurrentStateColor
        {
            get => _currentStateColor;
            set => SetProperty(ref _currentStateColor, value);
        }

        private readonly ITotalviewDataReaderCache _totalviewDataReaderCache;
        private readonly IMediator _mediator;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        private readonly ISubscription _reservationUpdatedMessageToken;
        private readonly ISubscription _reservationAddedNotificationToken;
        private readonly ISubscription _resourcesUpdatedMessageToken;

        /* ----------------------------------------------------------------------------  */
        /*                                 CONSTRUCTORS                                  */
        /* ----------------------------------------------------------------------------  */
        public HeaderViewModel(ITotalviewDataReaderCache totalviewDataReaderCache, IMediator mediator, AuthenticationStateProvider authenticationStateProvider)
        {
            _totalviewDataReaderCache = totalviewDataReaderCache ?? throw new ArgumentNullException(nameof(totalviewDataReaderCache));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _authenticationStateProvider = authenticationStateProvider ?? throw new ArgumentNullException(nameof(authenticationStateProvider));

            _resourcesUpdatedMessageToken = _mediator.Subscribe((ResourcesAddedNotification notification, CancellationToken ct) =>
            {
                if (_recId != null && notification.Resources.ContainsKey(_recId.Value))
                {
                    Update(notification.Resources[_recId.Value]);
                }

                return Task.CompletedTask;
            });

            _resourcesUpdatedMessageToken = _mediator.Subscribe((ResourcesUpdatedNotification notification, CancellationToken ct) =>
            {
                if (_recId != null && notification.Resources.ContainsKey(_recId.Value))
                {
                    Update(notification.Resources[_recId.Value]);
                }

                return Task.CompletedTask;
            });

            _reservationAddedNotificationToken = _mediator.Subscribe((ReservationAddedNotification notification, CancellationToken ct) =>
            {
                if (_recId != null && notification.ReservationsByRecourceId.ContainsKey(_recId.Value))
                {
                    Update(notification.ReservationsByRecourceId[_recId.Value]);
                }

                return Task.CompletedTask;
            });

            _reservationUpdatedMessageToken = _mediator.Subscribe((ReservationUpdatedNotification notification, CancellationToken ct) =>
            {
                if (_recId != null && notification.ReservationsByRecourceId.ContainsKey(_recId.Value))
                {
                    Update(notification.ReservationsByRecourceId[_recId.Value]);
                }

                return Task.CompletedTask;
            });

            _reservationUpdatedMessageToken = _mediator.Subscribe((StateUpdatedNotification notification, CancellationToken ct) =>
            {
                if (_currentStateId != null && notification.States.ContainsKey(_currentStateId.Value))
                {
                    Update(notification.States[_currentStateId.Value]);
                }

                return Task.CompletedTask;
            });

            _reservationUpdatedMessageToken = _mediator.Subscribe((StateAddedNotification notification, CancellationToken ct) =>
            {
                if (_currentStateId != null && notification.States.ContainsKey(_currentStateId.Value))
                {
                    Update(notification.States[_currentStateId.Value]);
                }

                return Task.CompletedTask;
            });
        }

        /* ----------------------------------------------------------------------------  */
        /*                                PUBLIC METHODS                                 */
        /* ----------------------------------------------------------------------------  */
        public override async Task OnInitializedAsync()
        {
            AuthenticationState authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            
            string? subject = authState.User.FindFirst("sub")?.Value;
            _ = int.TryParse(subject, out int recid);
            _recId = recid;

            Resource? foundResource = _totalviewDataReaderCache.Resources.Get(recid);
            Update(foundResource);
            await base.OnInitializedAsync();
        }

        /* ----------------------------------------------------------------------------  */
        /*                               PROTECTED METHODS                               */
        /* ----------------------------------------------------------------------------  */
        protected override void Dispose(bool disposing)
        {
            if(disposing)
            {
                _reservationAddedNotificationToken.Dispose();
                _reservationUpdatedMessageToken.Dispose();
                _resourcesUpdatedMessageToken.Dispose();
            }
        }

        /* ----------------------------------------------------------------------------  */
        /*                                PRIVATE METHODS                                */
        /* ----------------------------------------------------------------------------  */
        private void Update(Resource? resource)
        {
            FullName = resource != null ? $"{resource.FirstName} {resource.LastName}" : "Unknown";
        }

        private void Update(Reservation? reservation)
        {
            _currentStateId = reservation?.StartCode;
            CurrentStateText = reservation?.Subject ?? "";

            Update(reservation != null ? _totalviewDataReaderCache.States.Get(reservation.StartCode) : null);
        }

        private void Update(State? state)
        {
            if(state != null && string.IsNullOrWhiteSpace(CurrentStateText))
            {
                CurrentStateText = state.Caption;
            }

            CurrentStateColor = state?.GetHexColor() ?? "#000000";
        }
    }
}
