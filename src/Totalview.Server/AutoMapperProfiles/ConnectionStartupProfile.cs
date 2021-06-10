using AutoMapper;

namespace Totalview.Server.AutoMapperProfiles
{
    internal class ConnectionStartupProfile : Profile
    {
        public ConnectionStartupProfile()
        {
            //CreateMap<Reservation, Proxy.Streaming.T5StreamReservation>();
            CreateMap<Proxy.Streaming.T5StreamConnectionStartup, ConnectionStartup>();
        }
    }
}
