using System;
using System.Threading.Tasks;
using Totalview.Mediators;

namespace Totalview.BlazorClient.Client.Services
{
    public interface IDialogService
    {
        Task ShowSetCurrentStateDialogAsync();
    }

    public class DialogService : IDialogService
    {
        private readonly IMediator _mediator;

        public DialogService(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task ShowSetCurrentStateDialogAsync()
        {
            await _mediator.SendNotificationAsync(new ShowSetMyStateNotification());
        }
    }
}
