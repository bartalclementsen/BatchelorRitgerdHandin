using AutoMapper;

namespace Totalview.Server.AutoMapperProfiles
{
    internal class ReservationDetailProfile : Profile
    {
        public ReservationDetailProfile()
        {
            //CreateMap<Reservation, Proxy.Streaming.T5StreamReservation>();
            CreateMap<Proxy.Streaming.T5StreamReservationDetail, ReservationDetail>();
        }
    }
}
