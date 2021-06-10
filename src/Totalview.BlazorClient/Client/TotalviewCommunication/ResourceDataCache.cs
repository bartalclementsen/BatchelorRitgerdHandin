using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Totalview.BlazorClient.Client.Messages;
using Totalview.Mediators;
using Totalview.Server;

namespace Totalview.BlazorClient.Client.TotalviewCommunication
{
    public class ResourceDataCache : IDataCache<Resource>
    {
        /* ----------------------------------------------------------------------------  */
        /*                                  PROPERTIES                                   */
        /* ----------------------------------------------------------------------------  */
        private readonly Dictionary<int, Resource> _resources = new();

        private readonly ILogger<TotalviewDataCache> _logger;
        private readonly IMediator _mediator;

        public ResourceDataCache(ILogger<TotalviewDataCache> logger, IMediator mediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /* ----------------------------------------------------------------------------  */
        /*                                PUBLIC METHODS                                 */
        /* ----------------------------------------------------------------------------  */
        public Resource? Get(int id)
        {
            _resources.TryGetValue(id, out Resource? resource);
            return resource;
        }

        public IEnumerable<Resource> Get()
        {
            return _resources.Values;
        }

        public Task AddAsync(IEnumerable<Resource> resources) => AddAsync(resources, publishNotification: true);

        public Task RemoveAsync(IEnumerable<Resource> resources) => RemoveAsync(resources, publishNotification: true);

        public Task UpdateAsync(IEnumerable<Resource> resource) => UpdateAsync(resource, publishNotification: true);

        /* ----------------------------------------------------------------------------  */
        /*                                PRIVATE METHODS                                */
        /* ----------------------------------------------------------------------------  */
        private async Task AddAsync(IEnumerable<Resource> resources, bool publishNotification)
        {
            if (resources.Any() == false)
                return;

            List<Resource> addedResources = new();
            List<Resource> resourcesToUpdate = new();
            foreach (Resource? resource in resources)
            {
                bool wasAddedd = _resources.TryAdd(resource.RecId, resource);

                if (wasAddedd)
                {
                    addedResources.Add(resource);
                    _logger.LogDebug($"Resource {resource.UserId} added");
                }
                else
                {
                    resourcesToUpdate.Add(resource);
                }
            }

            if (publishNotification)
            {
                _logger.LogDebug($"{nameof(ResourcesAddedNotification)} published");
                await _mediator.SendNotificationAsync(new ResourcesAddedNotification(addedResources));
            }

            if (resourcesToUpdate.Any())
            {
                _ = UpdateAsync(resourcesToUpdate);
            }
        }

        private async Task RemoveAsync(IEnumerable<Resource> resources, bool publishNotification = true)
        {
            List<Resource> removedResources = new();
            foreach (Resource? resource in resources)
            {
                if (_resources.Remove(resource.RecId, out Resource? removedResource))
                {
                    _logger.LogInformation($"Removed resource {resource.UserId}");

                    if (removedResource != null)
                    {
                        removedResources.Add(removedResource);
                    }
                }
                else
                {
                    _logger.LogWarning($"Could not remove resource. Resource {resource.UserId} was not found");
                }
            }

            if (publishNotification)
            {
                _logger.LogDebug($"{nameof(ResourcesRemovedNotification)} published");
                await _mediator.SendNotificationAsync(new ResourcesRemovedNotification(removedResources));
            }
        }

        private async Task UpdateAsync(IEnumerable<Resource> resource, bool publishNotification = true)
        {
            if (resource.Any() == false)
                return;

            await RemoveAsync(resource, publishNotification: false);
            await AddAsync(resource, publishNotification: false);

            if (publishNotification)
            {
                _logger.LogDebug($"{nameof(ResourcesUpdatedNotification)} published");
                await _mediator.SendNotificationAsync(new ResourcesUpdatedNotification(resource));
            }
        }
    }
}
