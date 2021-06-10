using AutoMapper;

namespace Totalview.Server.AutoMapperProfiles
{
    internal class LargeDeviceListProfile : Profile
    {
        public LargeDeviceListProfile()
        {
            //CreateMap<Reservation, Proxy.Streaming.T5StreamReservation>();
            CreateMap<Proxy.Streaming.T5StreamLargeDeviceList, LargeDeviceList>()
                .ForMember(p => p.Items, p => p.MapFrom((p, s) =>
                {
                    return p.ItemList;
                }));
        }
    }
}
