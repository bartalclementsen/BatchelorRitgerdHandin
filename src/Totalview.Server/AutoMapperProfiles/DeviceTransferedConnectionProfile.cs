using AutoMapper;

namespace Totalview.Server.AutoMapperProfiles
{
    internal class DeviceTransferedConnectionProfile : Profile
    {
        public DeviceTransferedConnectionProfile()
        {
            //CreateMap<Reservation, Proxy.Streaming.T5StreamReservation>();
            CreateMap<Proxy.Streaming.T5StreamDeviceTransferedConnection, DeviceTransferedConnection>();
        }
    }
}
