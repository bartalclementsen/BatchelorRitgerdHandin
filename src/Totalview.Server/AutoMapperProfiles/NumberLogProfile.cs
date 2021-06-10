using AutoMapper;

namespace Totalview.Server.AutoMapperProfiles
{
    internal class NumberLogProfile : Profile
    {
        public NumberLogProfile()
        {
            //CreateMap<Reservation, Proxy.Streaming.T5StreamReservation>();
            CreateMap<Proxy.Streaming.T5StreamNumberLog, NumberLog>();
        }
    }
}
