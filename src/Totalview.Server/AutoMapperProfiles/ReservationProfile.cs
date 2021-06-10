using AutoMapper;

namespace Totalview.Server.AutoMapperProfiles
{
    internal class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            //CreateMap<Reservation, Proxy.Streaming.T5StreamReservation>();
            CreateMap<Proxy.Streaming.T5StreamReservation, Reservation>();
        }
    }
}
