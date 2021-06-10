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
    public class StateDataCache : IDataCache<State>
    {
        /* ----------------------------------------------------------------------------  */
        /*                                  PROPERTIES                                   */
        /* ----------------------------------------------------------------------------  */
        private readonly Dictionary<int, State> _states = new();

        private readonly ILogger<StateDataCache> _logger;
        private readonly IMediator _mediator;

        /* ----------------------------------------------------------------------------  */
        /*                                 CONSTRUCTORS                                  */
        /* ----------------------------------------------------------------------------  */
        public StateDataCache(ILogger<StateDataCache> logger, IMediator mediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /* ----------------------------------------------------------------------------  */
        /*                                PUBLIC METHODS                                 */
        /* ----------------------------------------------------------------------------  */
        public State? Get(int id)
        {
            _states.TryGetValue(id, out State? state);
            return state;
        }

        public IEnumerable<State> Get()
        {
            return _states.Values;
        }

        public Task AddAsync(IEnumerable<State> states) => AddAsync(states, publishNotification: true);

        public Task RemoveAsync(IEnumerable<State> states) => RemoveAsync(states, publishNotification: true);

        public Task UpdateAsync(IEnumerable<State> states) => UpdateAsync(states, publishNotification: true);

        /* ----------------------------------------------------------------------------  */
        /*                                PRIVATE METHODS                                */
        /* ----------------------------------------------------------------------------  */
        private async Task AddAsync(IEnumerable<State> states, bool publishNotification)
        {
            if (states.Any() == false)
                return;

            List<State> added = new();
            List<State> toUpdate = new();
            foreach (State? item in states)
            {
                bool wasAddedd = _states.TryAdd(item.StateId, item);

                if (wasAddedd)
                {
                    added.Add(item);
                    _logger.LogDebug($"State {item.StateId} added");
                }
                else
                {
                    toUpdate.Add(item);
                }
            }

            if (publishNotification)
            {
                _logger.LogDebug($"{nameof(StateAddedNotification)} published");
                await _mediator.SendNotificationAsync(new StateAddedNotification(added));
            }

            if (toUpdate.Any())
            {
                _ = UpdateAsync(toUpdate);
            }
        }

        private async Task RemoveAsync(IEnumerable<State> states, bool publishNotification = true)
        {
            List<State> removed = new();
            foreach (State? state in states)
            {
                if (_states.Remove(state.StateId, out State? removedState))
                {
                    _logger.LogInformation($"Removed state {removedState.StateId}");

                    if (removedState != null)
                    {
                        removed.Add(removedState);
                    }
                }
                else
                {
                    _logger.LogWarning($"Could not remove state. State {state.StateId} was not found");
                }
            }

            if (publishNotification)
            {
                _logger.LogDebug($"{nameof(StateRemovedNotification)} published");
                await _mediator.SendNotificationAsync(new StateRemovedNotification(removed));
            }
        }

        public async Task UpdateAsync(IEnumerable<State> states, bool publishNotification = true)
        {
            if (states.Any() == false)
                return;

            await RemoveAsync(states, publishNotification: false);
            await AddAsync(states, publishNotification: false);

            if (publishNotification)
            {
                _logger.LogDebug($"{nameof(StateUpdatedNotification)} published");
                await _mediator.SendNotificationAsync(new StateUpdatedNotification(states));
            }
        }
    }
}
