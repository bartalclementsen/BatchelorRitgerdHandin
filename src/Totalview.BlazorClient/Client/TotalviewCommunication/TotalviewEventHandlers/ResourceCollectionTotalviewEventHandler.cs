using System;
using System.Linq;
using System.Threading.Tasks;
using Totalview.Server;

namespace Totalview.BlazorClient.Client.TotalviewCommunication
{
    public class ResourceCollectionTotalviewEventHandler : ITotalviewEventHandler
    {
        private readonly IDataCache<Resource> _resourceDataCache;

        public ResourceCollectionTotalviewEventHandler(IDataCache<Resource> resourceDataCache)
        {
            _resourceDataCache = resourceDataCache ?? throw new ArgumentNullException(nameof(resourceDataCache));
        }

        public async Task<bool> HandleTotalviewEventAsync(TotalviewEvent totalviewEvent)
        {
            if (totalviewEvent?.ResourceCollection?.Items?.Any() != true)
            {
                return false;
            }

            Verb verb = totalviewEvent.Verb;

            switch (verb)
            {
                case Verb.Post:
                    await _resourceDataCache.AddAsync(totalviewEvent.ResourceCollection.Items);
                    break;
                case Verb.Put:
                    await _resourceDataCache.UpdateAsync(totalviewEvent.ResourceCollection.Items);
                    break;
                case Verb.Delete:
                    await _resourceDataCache.RemoveAsync(totalviewEvent.ResourceCollection.Items);
                    break;
            }

            return true;
        }
    }
}
