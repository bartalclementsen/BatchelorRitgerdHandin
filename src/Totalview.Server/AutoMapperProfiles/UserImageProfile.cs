using AutoMapper;

namespace Totalview.Server.AutoMapperProfiles
{
    internal class UserImageProfile : Profile
    {
        public UserImageProfile()
        {
            //CreateMap<Reservation, Proxy.Streaming.T5StreamReservation>();
            CreateMap<Proxy.Streaming.T5StreamUserImage, UserImage>();
        }
    }
}
