using System.Collections.Generic;
using Totalview.Server;

namespace Totalview.BlazorClient.Client.Messages
{
    public class ReservationUpdatedNotification : ReservationNotification
    {
        public ReservationUpdatedNotification(IEnumerable<Reservation> reservations) : base(reservations) { }
    }
}
