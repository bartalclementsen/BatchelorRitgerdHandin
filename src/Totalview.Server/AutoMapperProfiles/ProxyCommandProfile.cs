using AutoMapper;

namespace Totalview.Server.AutoMapperProfiles
{
    internal class ProxyCommandProfile : Profile
    {
        public ProxyCommandProfile()
        {
            //CreateMap<Reservation, Proxy.Streaming.T5StreamReservation>();
            CreateMap<Proxy.Streaming.T5StreamProxyCommand, ProxyCommand>();
        }
    }
}
