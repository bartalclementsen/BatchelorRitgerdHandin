using System;
using System.Collections.Generic;
using System.Linq;
using Totalview.Mediators;
using Totalview.Server;

namespace Totalview.BlazorClient.Client.Messages
{
    public abstract class ReservationNotification : INotification
    {
        public IDictionary<int, Reservation> Reservations { get; }

        public IDictionary<int, Reservation> ReservationsByRecourceId { get; }

        public ReservationNotification(IEnumerable<Reservation> reservations)
        {
            Reservations = reservations?.ToDictionary(r => r.Id, r => r) ?? throw new ArgumentNullException(nameof(reservations));
            ReservationsByRecourceId = reservations?.ToDictionary(r => r.ResourceId, r => r) ?? throw new ArgumentNullException(nameof(reservations));
        }
    }
}
