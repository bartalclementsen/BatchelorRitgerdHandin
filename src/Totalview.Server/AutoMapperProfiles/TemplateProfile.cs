﻿using AutoMapper;

namespace Totalview.Server.AutoMapperProfiles
{
    internal class TemplateProfile : Profile
    {
        public TemplateProfile()
        {
            //CreateMap<Reservation, Proxy.Streaming.T5StreamReservation>();
            CreateMap<Proxy.Streaming.T5StreamTemplate, Template>();
        }
    }
}
