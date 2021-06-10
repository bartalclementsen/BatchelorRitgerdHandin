using AutoMapper;

namespace Totalview.Server.AutoMapperProfiles
{
    internal class NotifyCommandProfile : Profile
    {
        public NotifyCommandProfile()
        {
            //CreateMap<Reservation, Proxy.Streaming.T5StreamReservation>();
            CreateMap<Proxy.Streaming.T5StreamNotifyCommand, NotifyCommand>();
        }
    }
}
