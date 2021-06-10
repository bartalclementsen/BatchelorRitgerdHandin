using AutoMapper;

namespace Totalview.Server.AutoMapperProfiles
{
    internal class StateDetailCollectionProfile : Profile
    {
        public StateDetailCollectionProfile()
        {
            //CreateMap<Reservation, Proxy.Streaming.T5StreamReservation>();
            CreateMap<Proxy.Streaming.T5StreamStateDetailCollection, StateDetailCollection>()
                .ForMember(p => p.Items, p => p.MapFrom((p, s) =>
                {
                    return p.ItemList;
                }));
        }
    }
}
