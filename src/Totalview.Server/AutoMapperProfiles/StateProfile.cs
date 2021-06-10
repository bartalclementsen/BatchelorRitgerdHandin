using AutoMapper;

namespace Totalview.Server.AutoMapperProfiles
{
    internal class StateProfile : Profile
    {
        public StateProfile()
        {
            //CreateMap<Reservation, Proxy.Streaming.T5StreamReservation>();
            CreateMap<Proxy.Streaming.T5StreamState, State>();
        }
    }
}
