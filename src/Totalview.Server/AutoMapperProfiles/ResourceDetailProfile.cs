using AutoMapper;

namespace Totalview.Server.AutoMapperProfiles
{
    internal class ResourceDetailProfile : Profile
    {
        public ResourceDetailProfile()
        {
            //CreateMap<Reservation, Proxy.Streaming.T5StreamReservation>();
            CreateMap<Proxy.Streaming.T5StreamResourceDetail, ResourceDetail>();
        }
    }
}
