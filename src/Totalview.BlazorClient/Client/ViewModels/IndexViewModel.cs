using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Totalview.BlazorClient.Client.TotalviewCommunication;
using Totalview.BlazorMvvm;

namespace Totalview.BlazorClient.Client.ViewModels
{
    public class IndexViewModel : ViewModelBase
    {
        /* ----------------------------------------------------------------------------  */
        /*                                  PROPERTIES                                   */
        /* ----------------------------------------------------------------------------  */
        private AccessTokenResult? _accessTokenResult;
        public AccessTokenResult? AccessTokenResult
        {
            get => _accessTokenResult;
            set => SetProperty(ref _accessTokenResult, value);
        }

        private AccessToken? _accessToken;
        public AccessToken? AccessToken
        {
            get => _accessToken;
            set => SetProperty(ref _accessToken, value);
        }

        private IIdentity? _identity;
        public IIdentity? Identity
        {
            get => _identity;
            set => SetProperty(ref _identity, value);
        }

        private IEnumerable<Claim>? _claims;
        public IEnumerable<Claim>? Claims
        {
            get => _claims;
            set => SetProperty(ref _claims, value);
        }

        private readonly ILogger<IndexViewModel> _logger;
        private readonly ITotalviewCommunicationService _totalviewCommunicationService;
        private readonly IAccessTokenProvider _accessTokenProvider;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        /* ----------------------------------------------------------------------------  */
        /*                                 CONSTRUCTORS                                  */
        /* ----------------------------------------------------------------------------  */
        public IndexViewModel(ILogger<IndexViewModel> logger, ITotalviewCommunicationService totalviewCommunicationService, IAccessTokenProvider accessTokenProvider, AuthenticationStateProvider authenticationStateProvider)
        {
            _logger = logger;
            _totalviewCommunicationService = totalviewCommunicationService ?? throw new ArgumentNullException(nameof(totalviewCommunicationService));
            _accessTokenProvider = accessTokenProvider ?? throw new ArgumentNullException(nameof(accessTokenProvider));
            _authenticationStateProvider = authenticationStateProvider ?? throw new ArgumentNullException(nameof(authenticationStateProvider));
        }

        /* ----------------------------------------------------------------------------  */
        /*                                PUBLIC METHODS                                 */
        /* ----------------------------------------------------------------------------  */
        public override async Task OnInitializedAsync()
        {
            // 1) If address is not stored then show the Choose Server view
            string address = "https://localhost:5003"; // We will just hardcode the address for now
            await _totalviewCommunicationService.ConnectAsync(address);

            // 2) If user is not logged in, then show the login view
            // For this prototype the user is automatically logged in

            // 3) Just show the default view

            AccessTokenResult = await _accessTokenProvider.RequestAccessToken();
            AccessTokenResult.TryGetToken(out _accessToken);
            RaisePropertyChanged(nameof(AccessToken));

            AuthenticationState authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            Identity = authState.User?.Identity;
            Claims = authState.User?.Claims;

            await base.OnInitializedAsync();
        }

    }
}
