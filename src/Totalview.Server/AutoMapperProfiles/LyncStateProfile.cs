using AutoMapper;

namespace Totalview.Server.AutoMapperProfiles
{
    internal class LyncStateProfile : Profile
    {
        public LyncStateProfile()
        {
            //CreateMap<Reservation, Proxy.Streaming.T5StreamReservation>();
            CreateMap<Proxy.Streaming.T5StreamLyncState, LyncState>();
        }
    }
}
