using AutoMapper;

namespace Totalview.Server.AutoMapperProfiles
{
    internal class DeviceLProfile : Profile
    {
        public DeviceLProfile()
        {
            //CreateMap<Reservation, Proxy.Streaming.T5StreamReservation>();
            CreateMap<Proxy.Streaming.T5StreamDeviceL, DeviceL>();
        }
    }
}
