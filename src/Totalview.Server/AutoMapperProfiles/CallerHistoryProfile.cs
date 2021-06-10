using AutoMapper;

namespace Totalview.Server.AutoMapperProfiles
{
    internal class CallerHistoryProfile : Profile
    {
        public CallerHistoryProfile()
        {
            //CreateMap<Reservation, Proxy.Streaming.T5StreamReservation>();
            CreateMap<Proxy.Streaming.T5StreamCallerHistory, CallerHistory>();
        }
    }
}
