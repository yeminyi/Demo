using AutoMapper;
using CodeTestDemo.Core.Entities;
using CodeTestDemo.Infrastructure.Resources;

namespace CodeTestDemo.Api.Extensions
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Score, ScoreResource>()
                .ForMember(dest => dest.UpdateTime, opt => opt.MapFrom(src => src.LastModified));
            CreateMap<ScoreResource, Score>();
            CreateMap<ScoreAddResource, Score>();
          
        }
    
    }
}

