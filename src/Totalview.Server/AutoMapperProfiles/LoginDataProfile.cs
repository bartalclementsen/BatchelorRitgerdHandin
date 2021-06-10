using AutoMapper;

namespace Totalview.Server.AutoMapperProfiles
{
    internal class LoginDataProfile : Profile
    {
        public LoginDataProfile()
        {
            //CreateMap<Reservation, Proxy.Streaming.T5StreamReservation>();
            CreateMap<Proxy.Streaming.T5StreamLoginData, LoginData>();
        }
    }
}
