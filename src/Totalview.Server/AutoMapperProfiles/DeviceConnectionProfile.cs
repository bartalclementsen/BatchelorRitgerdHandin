using AutoMapper;

namespace Totalview.Server.AutoMapperProfiles
{
    internal class DeviceConnectionProfile : Profile
    {
        public DeviceConnectionProfile()
        {
            //CreateMap<Reservation, Proxy.Streaming.T5StreamReservation>();
            CreateMap<Proxy.Streaming.T5StreamDeviceConnection, DeviceConnection>();
        }
    }
}
