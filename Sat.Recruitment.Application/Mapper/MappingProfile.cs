using AutoMapper;
using Sat.Recruitment.Application.Model;
using Sat.Recruitment.Domain.Entities;

namespace Sat.Recruitment.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}
