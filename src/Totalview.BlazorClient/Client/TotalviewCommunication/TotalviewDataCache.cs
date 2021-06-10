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
    public interface ITotalviewDataReaderCache
    {
        IDataCacheReader<Resource> Resources { get; }

        IDataCacheReader<Reservation> Reservations { get; }

        IDataCacheReader<State> States { get; }
    }

    public interface ITotalviewDataCache
    {
        IDataCache<Resource> ResourceDataCache { get; }

        IDataCache<Reservation> ReservationDataCache { get; }

        IDataCache<State> StateDataCache { get; }
    }

    public class TotalviewDataCache : ITotalviewDataCache, ITotalviewDataReaderCache
    {
        /* ----------------------------------------------------------------------------  */
        /*                                  PROPERTIES                                   */
        /* ----------------------------------------------------------------------------  */
        public IDataCache<Resource> ResourceDataCache { get; }

        public IDataCacheReader<Resource> Resources => ResourceDataCache;

        public IDataCache<Reservation> ReservationDataCache { get; }

        public IDataCacheReader<Reservation> Reservations => ReservationDataCache;

        public IDataCache<State> StateDataCache { get; }

        public IDataCacheReader<State> States => StateDataCache;

        /* ----------------------------------------------------------------------------  */
        /*                                 CONSTRUCTORS                                  */
        /* ----------------------------------------------------------------------------  */
        public TotalviewDataCache(
            IDataCache<Resource> resourceDataCache, 
            IDataCache<Reservation> reservationDataCache,
            IDataCache<State> stateDataCache)
        {
            ResourceDataCache = resourceDataCache ?? throw new ArgumentNullException(nameof(resourceDataCache));
            ReservationDataCache = reservationDataCache ?? throw new ArgumentNullException(nameof(reservationDataCache));
            StateDataCache = stateDataCache ?? throw new ArgumentNullException(nameof(stateDataCache));
        }
    }
}
