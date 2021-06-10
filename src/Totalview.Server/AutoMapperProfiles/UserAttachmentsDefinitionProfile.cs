using AutoMapper;

namespace Totalview.Server.AutoMapperProfiles
{
    internal class UserAttachmentsDefinitionProfile : Profile
    {
        public UserAttachmentsDefinitionProfile()
        {
            //CreateMap<Reservation, Proxy.Streaming.T5StreamReservation>();
            CreateMap<Proxy.Streaming.T5StreamUserAttachmentsDefinition, UserAttachmentsDefinition>();
        }
    }
}
