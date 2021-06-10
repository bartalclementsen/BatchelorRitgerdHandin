using AutoMapper;

namespace Totalview.Server.AutoMapperProfiles
{
    internal class ReservationUserProfile : Profile
    {
        public ReservationUserProfile()
        {
            //CreateMap<ReservationUser, Proxy.Streaming.T5StreamReservationUser>();
            CreateMap<Proxy.Streaming.T5StreamReservationUser, ReservationUser>();
        }
    }
}
