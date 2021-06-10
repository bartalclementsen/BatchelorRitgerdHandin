﻿using AutoMapper;

namespace Totalview.Server.AutoMapperProfiles
{
    internal class KeyValueListProfile : Profile
    {
        public KeyValueListProfile()
        {
            //CreateMap<Reservation, Proxy.Streaming.T5StreamReservation>();
            CreateMap<Proxy.Streaming.T5StreamKeyValueList, KeyValueList>()
                .ForMember(p => p.Items, p => p.MapFrom((p, s) =>
                {
                    return p.ItemList;
                }));
        }
    }
}
