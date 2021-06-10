using AutoMapper;

namespace Totalview.Server.AutoMapperProfiles
{
    internal class TemplateListProfile : Profile
    {
        public TemplateListProfile()
        {
            //CreateMap<Reservation, Proxy.Streaming.T5StreamReservation>();
            CreateMap<Proxy.Streaming.T5StreamTemplateList, TemplateList>()
                .ForMember(p => p.Items, p => p.MapFrom((p, s) =>
                {
                    return p.ItemList;
                }));
        }
    }
}
