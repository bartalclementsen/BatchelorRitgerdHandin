using Proxy.Streaming;
using System;
using Totalview.Mediators;

namespace Totalview.Server.Notifications
{
    internal class StreamReservationRecievedNotification : INotification
    {
        public T5StreamReservation StreamReservation { get; }

        public StreamReservationRecievedNotification(T5StreamReservation streamReservation)
        {
            StreamReservation = streamReservation ?? throw new ArgumentNullException(nameof(streamReservation));
        }
    }
}
