using AutoMapper;
using StudyVera.Application.Features.Auth.Commands;
using StudyVera.Application.Features.UserLessonProgresses.Commands;
using StudyVera.Contract.Dtos;
using StudyVera.Contract.Dtos.UserLessonProgress;
using StudyVera.Domain.Entities;
using StudyVera.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserForRegistrationDto, AppUser>().ReverseMap();
            CreateMap<LoginCommand, AppUser>().ReverseMap();

            CreateMap<Topic, TopicDto>().ReverseMap();

            CreateMap<UserLessonProgress, UserLessonProgressDto>()
                .ForMember(dest => dest.Topic, opt => opt.MapFrom(src => src.Topic))
                .ReverseMap()
                .ForMember(dest => dest.Topic, opt => opt.Ignore()); 

            CreateMap<AddUserLessonProgressCommand, UserLessonProgress>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.LastUpdated, opt => opt.Ignore())
                .ForMember(dest => dest.Topic, opt => opt.Ignore()) 
                .ForMember(dest => dest.User, opt => opt.Ignore())  
                .ReverseMap();
        }
    }

}
