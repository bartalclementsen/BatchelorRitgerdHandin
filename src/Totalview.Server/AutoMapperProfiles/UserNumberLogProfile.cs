using AutoMapper;

namespace Totalview.Server.AutoMapperProfiles
{
    internal class UserNumberLogProfile : Profile
    {
        public UserNumberLogProfile()
        {
            //CreateMap<Reservation, Proxy.Streaming.T5StreamReservation>();
            CreateMap<Proxy.Streaming.T5StreamUserNumberLog, UserNumberLog>();
        }
    }
}
