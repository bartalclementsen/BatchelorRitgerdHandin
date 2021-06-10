using AutoMapper;

namespace Totalview.Server.AutoMapperProfiles
{
    internal class ResourceProfile : Profile
    {
        public ResourceProfile()
        {
            //CreateMap<Reservation, Proxy.Streaming.T5StreamReservation>();
            CreateMap<Proxy.Streaming.T5StreamResource, Resource>();
        }
    }
}
