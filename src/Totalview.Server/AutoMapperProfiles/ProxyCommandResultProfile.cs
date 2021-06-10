using AutoMapper;

namespace Totalview.Server.AutoMapperProfiles
{
    internal class ProxyCommandResultProfile : Profile
    {
        public ProxyCommandResultProfile()
        {
            //CreateMap<Reservation, Proxy.Streaming.T5StreamReservation>();
            CreateMap<Proxy.Streaming.T5StreamProxyCommandResult, ProxyCommandResult>();
        }
    }
}
