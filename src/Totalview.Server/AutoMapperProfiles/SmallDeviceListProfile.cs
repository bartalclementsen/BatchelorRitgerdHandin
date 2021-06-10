using AutoMapper;

namespace Totalview.Server.AutoMapperProfiles
{
    internal class SmallDeviceListProfile : Profile
    {
        public SmallDeviceListProfile()
        {
            //CreateMap<Reservation, Proxy.Streaming.T5StreamReservation>();
            CreateMap<Proxy.Streaming.T5StreamSmallDeviceList, SmallDeviceList>()
                .ForMember(p => p.Items, p => p.MapFrom((p, s) =>
                {
                    return p.ItemList;
                }));
        }
    }
}
