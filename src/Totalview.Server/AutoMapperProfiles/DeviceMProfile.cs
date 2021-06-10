using AutoMapper;

namespace Totalview.Server.AutoMapperProfiles
{
    internal class DeviceMProfile : Profile
    {
        public DeviceMProfile()
        {
            //CreateMap<Reservation, Proxy.Streaming.T5StreamReservation>();
            CreateMap<Proxy.Streaming.T5StreamDeviceM, DeviceM>();
        }
    }
}
