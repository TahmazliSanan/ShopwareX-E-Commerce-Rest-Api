using AutoMapper;
using ShopwareX.Dtos.Gender;
using ShopwareX.Entities;

namespace ShopwareX.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GenderCreateDto, Gender>();
            CreateMap<Gender, GenderResponseDto>();
        }
    }
}
