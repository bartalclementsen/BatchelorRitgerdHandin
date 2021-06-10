using System;
using Totalview.BlazorClient.Client.Services;
using Totalview.BlazorMvvm;

namespace Totalview.BlazorClient.Client.ViewModels
{
    public class NavMenuViewModel : ViewModelBase
    {
        private readonly IDialogService _dialogService;

        public NavMenuViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));
        }

        public async void ChangeStatedClicked()
        {
            await _dialogService.ShowSetCurrentStateDialogAsync();
        }
    }
}
