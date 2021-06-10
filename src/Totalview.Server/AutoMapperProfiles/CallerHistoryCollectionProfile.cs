using AutoMapper;

namespace Totalview.Server.AutoMapperProfiles
{
    internal class CallerHistoryCollectionProfile : Profile
    {
        public CallerHistoryCollectionProfile()
        {
            //CreateMap<Reservation, Proxy.Streaming.T5StreamReservation>();
            CreateMap<Proxy.Streaming.T5StreamCallerHistoryCollection, CallerHistoryCollection>()
                .ForMember(p => p.Items, p => p.MapFrom((p, s) =>
                {
                    return p.ItemList;
                }));
        }
    }
}
