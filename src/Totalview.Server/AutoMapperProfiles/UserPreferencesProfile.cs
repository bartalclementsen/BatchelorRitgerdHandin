using AutoMapper;

namespace Totalview.Server.AutoMapperProfiles
{
    internal class UserPreferencesProfile : Profile
    {
        public UserPreferencesProfile()
        {
            //CreateMap<Reservation, Proxy.Streaming.T5StreamReservation>();
            CreateMap<Proxy.Streaming.T5StreamUserPreferences, UserPreferences>();
        }
    }
}
