using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telerik.DataSource.Extensions;
using Totalview.BlazorClient.Client.Messages;
using Totalview.BlazorClient.Client.TotalviewCommunication;
using Totalview.BlazorMvvm;
using Totalview.Mediators;
using Totalview.Server;

namespace Totalview.BlazorClient.Client.ViewModels
{
    public class ResourcesViewModel : ViewModelBase
    {
        /* ----------------------------------------------------------------------------  */
        /*                                  PROPERTIES                                   */
        /* ----------------------------------------------------------------------------  */
        public SmartObservableCollection<ResourceViewModel> Resources { get; set; } = new();

        public int Count { get; set; } = 100;

        private readonly ILogger<ResourcesViewModel> _logger;
        private readonly ITotalviewDataReaderCache _totalviewDataReaderCache;
        private readonly IMediator _mediator;
        private readonly Func<ResourceViewModel> _resourceViewModelFactory;

        private readonly IDisposable _resourcesAddedMessageToken;
        private readonly IDisposable _resourcesRemovedMessageToken;

        /* ----------------------------------------------------------------------------  */
        /*                                 CONSTRUCTORS                                  */
        /* ----------------------------------------------------------------------------  */
        public ResourcesViewModel(ILogger<ResourcesViewModel> logger, ITotalviewDataReaderCache totalviewDataReaderCache, IMediator mediator, Func<ResourceViewModel> resourceViewModelFactory)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _totalviewDataReaderCache = totalviewDataReaderCache ?? throw new ArgumentNullException(nameof(totalviewDataReaderCache));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _resourceViewModelFactory = resourceViewModelFactory ?? throw new ArgumentNullException(nameof(resourceViewModelFactory));

            _resourcesAddedMessageToken = _mediator.Subscribe((ResourcesAddedNotification notification, CancellationToken ct) =>
            {
                AddOrUpdate(notification.Resources);
                return Task.CompletedTask;
            });

            _resourcesRemovedMessageToken = _mediator.Subscribe((ResourcesRemovedNotification notification, CancellationToken ct) =>
            {
                Remove(notification.Resources);
                return Task.CompletedTask;
            });
        }

        /* ----------------------------------------------------------------------------  */
        /*                                PUBLIC METHODS                                 */
        /* ----------------------------------------------------------------------------  */
        public override void OnInitialized()
        {
            foreach (var resource in _totalviewDataReaderCache.Resources.Get().OrderBy(r => r.FirstName).ThenBy(r => r.LastName))
            {
                var resourceViewModel = _resourceViewModelFactory.Invoke();
                resourceViewModel.PropertyChanged += (o, e) =>
                {

                };

                resourceViewModel.Update(resource);
                Resources.Add(resourceViewModel);
            }


            Dictionary<int, Reservation> reservations = _totalviewDataReaderCache.Reservations.Get().ToDictionary(r => r.ResourceId, r => r);
            if (reservations.Any())
            {
                foreach (var resourceViewModel in Resources.Where(r => r.RecId.HasValue))
                {
                    if (reservations.ContainsKey(resourceViewModel.RecId!.Value))
                    {
                        resourceViewModel.Update(reservations[resourceViewModel.RecId!.Value]);
                    }
                }
            }
        }

        /* ----------------------------------------------------------------------------  */
        /*                               PROTECTED METHODS                               */
        /* ----------------------------------------------------------------------------  */
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _resourcesAddedMessageToken.Dispose();
                _resourcesRemovedMessageToken.Dispose();
            }
        }

        /* ----------------------------------------------------------------------------  */
        /*                                PRIVATE METHODS                                */
        /* ----------------------------------------------------------------------------  */
        private void AddOrUpdate(IDictionary<int, Resource>? resources)
        {
            if (resources?.Any() != true)
            {
                return;
            }

            foreach (Resource resource in resources.Values)
            {
                ResourceViewModel? foundResource = Resources.FirstOrDefault(r => r.RecId == resource.RecId);
                if (foundResource == null)
                {
                    foundResource = _resourceViewModelFactory.Invoke();
                    Resources.Add(foundResource);
                }

                foundResource.Update(resource);
            }

            Count = Resources.Count();
        }

        private void Remove(IDictionary<int, Resource>? resources)
        {
            if (resources?.Any() != true)
            {
                return;
            }

            foreach (Resource resource in resources.Values)
            {
                ResourceViewModel? foundResource = Resources.FirstOrDefault(r => r.RecId == resource.RecId);
                if (foundResource != null)
                {
                    Resources.Remove(foundResource);
                }
            }
        }
    }
}
