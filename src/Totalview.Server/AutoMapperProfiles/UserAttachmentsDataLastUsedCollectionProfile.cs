using AutoMapper;

namespace Totalview.Server.AutoMapperProfiles
{
    internal class UserAttachmentsDataLastUsedCollectionProfile : Profile
    {
        public UserAttachmentsDataLastUsedCollectionProfile()
        {
            //CreateMap<Reservation, Proxy.Streaming.T5StreamReservation>();
            CreateMap<Proxy.Streaming.T5StreamUserAttachmentsDataLastUsedCollection, UserAttachmentsDataLastUsedCollection>()
                .ForMember(p => p.Items, p => p.MapFrom((p, s) =>
                {
                    return p.ItemList;
                }));
        }
    }
}
