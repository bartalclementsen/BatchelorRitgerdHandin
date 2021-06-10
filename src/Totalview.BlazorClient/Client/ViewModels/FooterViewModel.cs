using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System;
using System.Threading.Tasks;
using Totalview.BlazorMvvm;

namespace Totalview.BlazorClient.Client.ViewModels
{
    public class FooterViewModel : ViewModelBase
    {
        /* ----------------------------------------------------------------------------  */
        /*                                  PROPERTIES                                   */
        /* ----------------------------------------------------------------------------  */
        private string? _fullName;
        public string? FullName
        {
            get => _fullName;
            set => SetProperty(ref _fullName, value);
        }

        private readonly NavigationManager _navigation;
        private readonly SignOutSessionStateManager _signOutManager;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        /* ----------------------------------------------------------------------------  */
        /*                                 CONSTRUCTORS                                  */
        /* ----------------------------------------------------------------------------  */
        public FooterViewModel(NavigationManager navigation, SignOutSessionStateManager signOutManager, AuthenticationStateProvider authenticationStateProvider)
        {
            _navigation = navigation ?? throw new ArgumentNullException(nameof(navigation));
            _signOutManager = signOutManager ?? throw new ArgumentNullException(nameof(signOutManager));
            _authenticationStateProvider = authenticationStateProvider ?? throw new ArgumentNullException(nameof(authenticationStateProvider));
        }

        /* ----------------------------------------------------------------------------  */
        /*                                PUBLIC METHODS                                 */
        /* ----------------------------------------------------------------------------  */
        public async Task SignoutButton_Clicked(MouseEventArgs args)
        {
            await _signOutManager.SetSignOutState();
            _navigation.NavigateTo("authentication/logout");
        }

        public override async Task OnInitializedAsync()
        {
            AuthenticationState authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            FullName = authState.User?.Identity?.Name ?? "Unknown";

            await base.OnInitializedAsync();
        }
    }
}
