using AutoMapper;

namespace Totalview.Server.AutoMapperProfiles
{
    internal class ReservationCollectionProfile : Profile
    {
        public ReservationCollectionProfile()
        {
            //CreateMap<ReservationCollection, Proxy.Streaming.T5StreamReservationCollection>();
            CreateMap<Proxy.Streaming.T5StreamReservationCollection, ReservationCollection>()
                .ForMember(p => p.Items, p => p.MapFrom((p, s) =>
                {
                    return p.ItemList;
                }));
        }
    }
}
