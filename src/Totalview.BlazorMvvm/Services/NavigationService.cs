using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using System;
using System.Linq;

namespace Totalview.BlazorMvvm
{
    public interface INavigationService
    {
        string Uri { get; }

        string BaseUri { get; }

        event EventHandler<LocationChangedEventArgs> LocationChanged;

        void Navigate<T>() where T : ViewModelBase;

        void Navigate<T>(params string[] args) where T : ViewModelBase;

        void Navigate<T>(bool forceLoad = false, params string[] args) where T : ViewModelBase;

        void Navigate(string uri, bool forceLoad = false, params string[]? args);
    }

    public class NavigationService : INavigationService
    {
        /* ----------------------------------------------------------------------------  */
        /*                                CUSTOM EVENTS                                  */
        /* ----------------------------------------------------------------------------  */
        public event EventHandler<LocationChangedEventArgs>? LocationChanged;

        /* ----------------------------------------------------------------------------  */
        /*                                  PROPERTIES                                   */
        /* ----------------------------------------------------------------------------  */
        public string Uri => _navigationManager.Uri;

        public string BaseUri => _navigationManager.BaseUri;

        private readonly NavigationManager _navigationManager;

        /* ----------------------------------------------------------------------------  */
        /*                                 CONSTRUCTORS                                  */
        /* ----------------------------------------------------------------------------  */
        public NavigationService(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
            _navigationManager.LocationChanged += NavigationManager_LocationChanged;
        }

        /* ----------------------------------------------------------------------------  */
        /*                                PUBLIC METHODS                                 */
        /* ----------------------------------------------------------------------------  */
        public void Navigate<T>() where T : ViewModelBase
            => Navigate<T>(false, null);

        public void Navigate<T>(params string[] args) where T : ViewModelBase
            => Navigate<T>(false, args);

        public void Navigate<T>(bool forceLoad = false, params string[]? args) where T : ViewModelBase
        {
            string uri = typeof(T).Name;
            uri = uri.Replace("ViewModel", "");

            if (uri == "Index")
                uri = "";

            Navigate(uri, forceLoad, args);
        }

        public void Navigate(string uri, bool forceLoad = false, params string[]? args)
        {
            if (args?.Where(a => string.IsNullOrWhiteSpace(a) == false).Any() == true)
            {
                uri += "/" + string.Join("/", args.Where(a => string.IsNullOrWhiteSpace(a) == false));
            }

            _navigationManager.NavigateTo(uri, forceLoad);
        }

        /* ----------------------------------------------------------------------------  */
        /*                                EVENT HANDLERS                                 */
        /* ----------------------------------------------------------------------------  */
        private void NavigationManager_LocationChanged(object? sender, LocationChangedEventArgs eventArgs)
        {
            LocationChanged?.Invoke(this, eventArgs);
        }
    }
}
