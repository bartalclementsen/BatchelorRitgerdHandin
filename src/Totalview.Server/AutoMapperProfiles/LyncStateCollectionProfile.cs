using AutoMapper;

namespace Totalview.Server.AutoMapperProfiles
{
    internal class LyncStateCollectionProfile : Profile
    {
        public LyncStateCollectionProfile()
        {
            //CreateMap<Reservation, Proxy.Streaming.T5StreamReservation>();
            CreateMap<Proxy.Streaming.T5StreamLyncStateCollection, LyncStateCollection>()
                .ForMember(p => p.Items, p => p.MapFrom((p, s) =>
                {
                    return p.ItemList;
                }));
        }
    }
}
