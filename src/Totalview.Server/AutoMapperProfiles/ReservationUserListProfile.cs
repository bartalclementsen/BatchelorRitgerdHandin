using AutoMapper;

namespace Totalview.Server.AutoMapperProfiles
{

    internal class ReservationUserListProfile : Profile
    {
        public ReservationUserListProfile()
        {
            //CreateMap<ReservationUserList, Proxy.Streaming.T5StreamReservationUserList>();

            CreateMap<Proxy.Streaming.T5StreamReservationUserList, ReservationUserList>()
                .ForMember(p => p.Items, p => p.MapFrom((p, s) =>
                {
                    //return new RepeatedField<ReservationUser>();
                    return p.ItemList;
                }));
        }
    }
}
