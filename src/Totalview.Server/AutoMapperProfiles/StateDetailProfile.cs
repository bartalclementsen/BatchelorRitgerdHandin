using AutoMapper;

namespace Totalview.Server.AutoMapperProfiles
{
    internal class StateDetailProfile : Profile
    {
        public StateDetailProfile()
        {
            //CreateMap<Reservation, Proxy.Streaming.T5StreamReservation>();
            CreateMap<Proxy.Streaming.T5StreamStateDetail, StateDetail>();
        }
    }
}
