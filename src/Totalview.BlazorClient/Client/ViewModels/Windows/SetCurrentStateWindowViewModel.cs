using System;
using System.Threading;
using Totalview.BlazorClient.Client.Services;
using Totalview.BlazorClient.Client.TotalviewCommunication;
using Totalview.BlazorMvvm;
using Totalview.Mediators;

namespace Totalview.BlazorClient.Client.ViewModels
{
    public class SetCurrentStateWindowViewModel : ViewModelBase
    {
        /* ----------------------------------------------------------------------------  */
        /*                                  PROPERTIES                                   */
        /* ----------------------------------------------------------------------------  */
        private bool _isVisible;
        public bool IsVisible
        {
            get => _isVisible;
            set => SetProperty(ref _isVisible, value);
        }

        private readonly IMediator _mediator;
        private readonly ITotalviewCommunicationService _totalviewCommunicationService;

        private readonly ISubscription subscription;

        /* ----------------------------------------------------------------------------  */
        /*                                 CONSTRUCTORS                                  */
        /* ----------------------------------------------------------------------------  */
        public SetCurrentStateWindowViewModel(IMediator mediator, ITotalviewCommunicationService totalviewCommunicationService)
        {
            _totalviewCommunicationService = totalviewCommunicationService ?? throw new ArgumentNullException(nameof(totalviewCommunicationService));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

            subscription = _mediator.Subscribe(async (ShowSetMyStateNotification notification, CancellationToken ct) =>
            {
                await _totalviewCommunicationService.SetCurrentState(new Server.SetCurrentStateRequest());
                IsVisible = true;
            });
        }
    }
}
