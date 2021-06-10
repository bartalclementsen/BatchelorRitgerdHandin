using AutoMapper;

namespace Totalview.Server.AutoMapperProfiles
{
    internal class UserImageListProfile : Profile
    {
        public UserImageListProfile()
        {
            //CreateMap<Reservation, Proxy.Streaming.T5StreamReservation>();
            CreateMap<Proxy.Streaming.T5StreamUserImageList, UserImageList>()
                .ForMember(p => p.Items, p => p.MapFrom((p, s) =>
                {
                    return p.ItemList;
                }));
        }
    }
}
