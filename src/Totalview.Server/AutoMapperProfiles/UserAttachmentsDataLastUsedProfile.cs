using AutoMapper;

namespace Totalview.Server.AutoMapperProfiles
{
    internal class UserAttachmentsDataLastUsedProfile : Profile
    {
        public UserAttachmentsDataLastUsedProfile()
        {
            //CreateMap<Reservation, Proxy.Streaming.T5StreamReservation>();
            CreateMap<Proxy.Streaming.T5StreamUserAttachmentsDataLastUsed, UserAttachmentsDataLastUsed>();
        }
    }
}
