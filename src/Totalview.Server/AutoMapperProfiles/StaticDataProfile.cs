using AutoMapper;

namespace Totalview.Server.AutoMapperProfiles
{
    internal class StaticDataProfile : Profile
    {
        public StaticDataProfile()
        {
            //CreateMap<Reservation, Proxy.Streaming.T5StreamReservation>();
            CreateMap<Proxy.Streaming.T5StreamStaticData, StaticData>();
        }
    }
}
