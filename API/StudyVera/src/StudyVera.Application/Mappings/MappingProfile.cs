using AutoMapper;
using StudyVera.Contract.Dtos;
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
            CreateMap<UserForAuthenticationDto, AppUser>().ReverseMap();
        }
    }

}
