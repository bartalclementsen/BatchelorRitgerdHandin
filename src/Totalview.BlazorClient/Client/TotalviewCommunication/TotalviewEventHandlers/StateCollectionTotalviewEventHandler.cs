using System;
using System.Linq;
using System.Threading.Tasks;
using Totalview.Server;

namespace Totalview.BlazorClient.Client.TotalviewCommunication
{
    public class StateCollectionTotalviewEventHandler : ITotalviewEventHandler
    {
        private readonly IDataCache<State> _stateDataCache;

        public StateCollectionTotalviewEventHandler(IDataCache<State> stateDataCache)
        {
            _stateDataCache = stateDataCache ?? throw new ArgumentNullException(nameof(stateDataCache));
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
                    await _stateDataCache.AddAsync(totalviewEvent.StateCollection.Items);
                    break;
                case Verb.Put:
                    await _stateDataCache.UpdateAsync(totalviewEvent.StateCollection.Items);
                    break;
                case Verb.Delete:
                    await _stateDataCache.RemoveAsync(totalviewEvent.StateCollection.Items);
                    break;
            }

            return true;
        }
    }
}
