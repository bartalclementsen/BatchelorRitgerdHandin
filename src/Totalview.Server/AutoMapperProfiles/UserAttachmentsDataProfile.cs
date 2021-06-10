using AutoMapper;

namespace Totalview.Server.AutoMapperProfiles
{
    internal class UserAttachmentsDataProfile : Profile
    {
        public UserAttachmentsDataProfile()
        {
            //CreateMap<Reservation, Proxy.Streaming.T5StreamReservation>();
            CreateMap<Proxy.Streaming.T5StreamUserAttachmentsData, UserAttachmentsData>();
        }
    }
}
