using System.Collections.Generic;
using Totalview.Server;

namespace Totalview.BlazorClient.Client.Messages
{
    public class ReservationRemoveddNotification : ReservationNotification
    {
        public ReservationRemoveddNotification(IEnumerable<Reservation> reservations) : base(reservations) { }
    }
}
