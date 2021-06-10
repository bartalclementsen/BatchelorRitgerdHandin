using System.Collections.Generic;
using Totalview.Server;

namespace Totalview.BlazorClient.Client.Messages
{
    public class ReservationAddedNotification : ReservationNotification
    {
        public ReservationAddedNotification(IEnumerable<Reservation> reservations) : base(reservations) { }
    }
}
