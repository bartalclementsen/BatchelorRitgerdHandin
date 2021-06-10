using AutoMapper;

namespace Totalview.Server.AutoMapperProfiles
{
    internal class KeyValueProfile : Profile
    {
        public KeyValueProfile()
        {
            //CreateMap<Reservation, Proxy.Streaming.T5StreamReservation>();
            CreateMap<Proxy.Streaming.T5StreamKeyValue, KeyValue>();
        }
    }
}
