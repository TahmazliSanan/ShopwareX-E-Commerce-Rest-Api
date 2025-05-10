using AutoMapper;
using ShopwareX.Dtos.Gender;
using ShopwareX.Dtos.Role;
using ShopwareX.Dtos.User;
using ShopwareX.Entities;

namespace ShopwareX.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GenderCreateDto, Gender>();
            CreateMap<GenderUpdateDto, Gender>();
            CreateMap<Gender, GenderResponseDto>();

            CreateMap<RoleCreateDto, Role>();
            CreateMap<RoleUpdateDto, Role>();
            CreateMap<Role, RoleResponseDto>();

            CreateMap<UserCreateDto, User>();
            CreateMap<UserUpdateDto, User>();
            CreateMap<User, UserResponseDto>()
                .ForMember(dest => dest.GenderName, opt => opt.MapFrom(src => src.Gender.Name))
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name));
        }
    }
}
