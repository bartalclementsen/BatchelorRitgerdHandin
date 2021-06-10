using AutoMapper;

namespace Totalview.Server.AutoMapperProfiles
{
    internal class ResourceCollectionProfile : Profile
    {
        public ResourceCollectionProfile()
        {
            //CreateMap<Reservation, Proxy.Streaming.T5StreamReservation>();
            CreateMap<Proxy.Streaming.T5StreamResourceCollection, ResourceCollection>()
                .ForMember(p => p.Items, p => p.MapFrom((p, s) =>
                {
                    return p.ItemList;
                }));
        }
    }
}
