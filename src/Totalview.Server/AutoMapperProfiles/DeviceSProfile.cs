using AutoMapper;

namespace Totalview.Server.AutoMapperProfiles
{
    internal class DeviceSProfile : Profile
    {
        public DeviceSProfile()
        {
            //CreateMap<Reservation, Proxy.Streaming.T5StreamReservation>();
            CreateMap<Proxy.Streaming.T5StreamDeviceS, DeviceS>();
        }
    }
}
