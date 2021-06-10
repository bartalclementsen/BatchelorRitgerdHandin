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
    public class ReservationDataCache : IDataCache<Reservation>
    {
        /* ----------------------------------------------------------------------------  */
        /*                                  PROPERTIES                                   */
        /* ----------------------------------------------------------------------------  */
        private readonly Dictionary<int, Reservation> _reservation = new();

        private readonly ILogger<TotalviewDataCache> _logger;
        private readonly IMediator _mediator;

        /* ----------------------------------------------------------------------------  */
        /*                                 CONSTRUCTORS                                  */
        /* ----------------------------------------------------------------------------  */
        public ReservationDataCache(ILogger<TotalviewDataCache> logger, IMediator mediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /* ----------------------------------------------------------------------------  */
        /*                                PUBLIC METHODS                                 */
        /* ----------------------------------------------------------------------------  */
        public Reservation? Get(int id)
        {
            _reservation.TryGetValue(id, out Reservation? reservation);
            return reservation;
        }

        public IEnumerable<Reservation> Get()
        {
            return _reservation.Values;
        }

        public Task AddAsync(IEnumerable<Reservation> reservations) => AddAsync(reservations, publishNotification: true);

        public Task RemoveAsync(IEnumerable<Reservation> reservations) => RemoveAsync(reservations, publishNotification: true);

        public Task UpdateAsync(IEnumerable<Reservation> reservations) => UpdateAsync(reservations, publishNotification: true);

        /* ----------------------------------------------------------------------------  */
        /*                                PRIVATE METHODS                                */
        /* ----------------------------------------------------------------------------  */
        private async Task AddAsync(IEnumerable<Reservation> reservations, bool publishNotification)
        {
            if (reservations.Any() == false)
                return;

            List<Reservation> added = new();
            List<Reservation> toUpdate = new();
            foreach (Reservation? item in reservations)
            {
                bool wasAddedd = _reservation.TryAdd(item.Id, item);

                if (wasAddedd)
                {
                    added.Add(item);
                    _logger.LogInformation($"Reservation {item.Id} added for resource {item.ResourceId}");
                }
                else
                {
                    toUpdate.Add(item);
                }
            }

            if (publishNotification)
            {
                _logger.LogDebug($"{nameof(ReservationAddedNotification)} published");
                await _mediator.SendNotificationAsync(new ReservationAddedNotification(added));
            }

            if (toUpdate.Any())
            {
                await UpdateAsync(toUpdate);
            }
        }

        private async Task RemoveAsync(IEnumerable<Reservation> reservations, bool publishNotification = true)
        {
            List<Reservation> removed = new();
            foreach (Reservation? reservation in reservations)
            {
                if (_reservation.Remove(reservation.Id, out Reservation? removedResource))
                {
                    _logger.LogInformation($"Removed reservation {reservation.Id}");

                    if (removedResource != null)
                    {
                        removed.Add(removedResource);
                    }
                }
                else
                {
                    _logger.LogWarning($"Could not remove reservation. Reservation {reservation.Id} was not found");
                }
            }

            if (publishNotification)
            {
                _logger.LogDebug($"{nameof(ReservationRemoveddNotification)} published");
                await _mediator.SendNotificationAsync(new ReservationRemoveddNotification(removed));
            }
        }

        private async Task UpdateAsync(IEnumerable<Reservation> reservations, bool publishNotification = true)
        {
            if (reservations.Any() == false)
                return;

            await RemoveAsync(reservations, publishNotification: false);
            await AddAsync(reservations, publishNotification: false);

            if (publishNotification)
            {
                _logger.LogDebug($"{nameof(ReservationUpdatedNotification)} published");
                await _mediator.SendNotificationAsync(new ReservationUpdatedNotification(reservations));
            }
        }
    }
}
