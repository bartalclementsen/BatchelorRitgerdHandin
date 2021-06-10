using AutoMapper;

namespace Totalview.Server.AutoMapperProfiles
{
    internal class StateCollectionProfile : Profile
    {
        public StateCollectionProfile()
        {
            //CreateMap<Reservation, Proxy.Streaming.T5StreamReservation>();
            CreateMap<Proxy.Streaming.T5StreamStateCollection, StateCollection>()
                .ForMember(p => p.Items, p => p.MapFrom((p, s) =>
                {
                    return p.ItemList;
                }));
        }
    }
}
