using System;
using Totalview.Mediators;

namespace Totalview.Server.Notifications
{
    internal class StreamReservationUserListRecievedNotification : INotification
    {
        public ReservationUserList ReservationUserList { get; }

        public StreamReservationUserListRecievedNotification(ReservationUserList reservationUserList)
        {
            ReservationUserList = reservationUserList ?? throw new ArgumentNullException(nameof(reservationUserList));
        }
    }
}
