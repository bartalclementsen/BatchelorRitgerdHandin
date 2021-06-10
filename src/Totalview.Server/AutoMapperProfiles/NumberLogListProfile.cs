using AutoMapper;

namespace Totalview.Server.AutoMapperProfiles
{
    internal class NumberLogListProfile : Profile
    {
        public NumberLogListProfile()
        {
            //CreateMap<Reservation, Proxy.Streaming.T5StreamReservation>();
            CreateMap<Proxy.Streaming.T5StreamNumberLogList, NumberLogList>()
                .ForMember(p => p.Items, p => p.MapFrom((p, s) =>
                {
                    return p.ItemList;
                }));
        }
    }
}
