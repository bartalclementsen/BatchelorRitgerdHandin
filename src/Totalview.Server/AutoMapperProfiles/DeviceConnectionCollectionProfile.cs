using AutoMapper;

namespace Totalview.Server.AutoMapperProfiles
{
    internal class DeviceConnectionCollectionProfile : Profile
    {
        public DeviceConnectionCollectionProfile()
        {
            //CreateMap<Reservation, Proxy.Streaming.T5StreamReservation>();
            CreateMap<Proxy.Streaming.T5StreamDeviceConnectionCollection, DeviceConnectionCollection>()
                .ForMember(p => p.Items, p => p.MapFrom((p, s) =>
                {
                    return p.ItemList;
                }));
        }
    }
}
