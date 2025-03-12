using AutoMapper;
using Core.DTOs;
using Entities.Concrete;

namespace Business.MappingProfiles
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<User,UserDto>().ReverseMap()
                 .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()) 
            .ForMember(dest => dest.PasswordSalt, opt => opt.Ignore());
        }
    }
}
