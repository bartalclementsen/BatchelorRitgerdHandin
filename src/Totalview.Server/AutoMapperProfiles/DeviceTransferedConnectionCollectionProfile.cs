using AutoMapper;

namespace Totalview.Server.AutoMapperProfiles
{
    internal class DeviceTransferedConnectionCollectionProfile : Profile
    {
        public DeviceTransferedConnectionCollectionProfile()
        {
            //CreateMap<Reservation, Proxy.Streaming.T5StreamReservation>();
            CreateMap<Proxy.Streaming.T5StreamDeviceTransferedConnectionCollection, DeviceTransferedConnectionCollection>()
                .ForMember(p => p.Items, p => p.MapFrom((p, s) =>
                {
                    return p.ItemList;
                }));
        }
    }
}
