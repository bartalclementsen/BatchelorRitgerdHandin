using AutoMapper;

namespace Totalview.Server.AutoMapperProfiles
{
    internal class ResourceDetailCollectionProfile : Profile
    {
        public ResourceDetailCollectionProfile()
        {
            //CreateMap<Reservation, Proxy.Streaming.T5StreamReservation>();
            CreateMap<Proxy.Streaming.T5StreamResourceDetailCollection, ResourceDetailCollection>()
                .ForMember(p => p.Items, p => p.MapFrom((p, s) =>
                {
                    return p.ItemList;
                }));
        }
    }
}
