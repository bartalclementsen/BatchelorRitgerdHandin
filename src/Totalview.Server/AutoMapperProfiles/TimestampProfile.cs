using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using System;

namespace Totalview.Server.AutoMapperProfiles
{
    internal class TimestampProfile : Profile
    {
        public TimestampProfile()
        {
            CreateMap<DateTime, Timestamp>()
                .ConvertUsing<DateTimeToTimestampConverter>();
        }

        public class DateTimeToTimestampConverter : ITypeConverter<DateTime, Timestamp>
        {
            public Timestamp Convert(DateTime source, Timestamp destination, ResolutionContext context)
            {
                return Timestamp.FromDateTime(source.ToUniversalTime());
            }
        }
    }
}
