using System;
using System.Linq;
using System.Threading.Tasks;
using Totalview.Server;

namespace Totalview.BlazorClient.Client.TotalviewCommunication
{
    public class ReservationCollectionTotalviewEventHandler : ITotalviewEventHandler
    {
        private readonly IDataCache<Reservation> _reservationDataCache;

        public ReservationCollectionTotalviewEventHandler(IDataCache<Reservation> reservationDataCache)
        {
            _reservationDataCache = reservationDataCache ?? throw new ArgumentNullException(nameof(reservationDataCache));
        }

        public async Task<bool> HandleTotalviewEventAsync(TotalviewEvent totalviewEvent)
        {
            if (totalviewEvent?.ReservationCollection?.Items?.Any() != true)
            {
                return false;
            }

            Verb verb = totalviewEvent.Verb;

            switch (verb)
            {
                case Verb.Post:
                    await _reservationDataCache.AddAsync(totalviewEvent.ReservationCollection.Items);
                    break;
                case Verb.Put:
                    await _reservationDataCache.UpdateAsync(totalviewEvent.ReservationCollection.Items);
                    break;
                case Verb.Delete:
                    await _reservationDataCache.RemoveAsync(totalviewEvent.ReservationCollection.Items);
                    break;
            }

            return true;
        }
    }
}
