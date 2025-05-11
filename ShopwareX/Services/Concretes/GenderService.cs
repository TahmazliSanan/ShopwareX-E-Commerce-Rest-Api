using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopwareX.Dtos.Gender;
using ShopwareX.Entities;
using ShopwareX.Repositories.Abstracts;
using ShopwareX.Services.Abstracts;

namespace ShopwareX.Services.Concretes
{
    public class GenderService(IMapper mapper, IGenderRepository genderRepository) : IGenderService
    {
        private readonly IMapper _mapper = mapper;
        private readonly IGenderRepository _genderRepository = genderRepository;

        public async Task<GenderResponseDto> AddGenderAsync(GenderCreateDto dto)
        {
            var gender = _mapper.Map<Gender>(dto);
            await _genderRepository.AddAsync(gender);
            await _genderRepository.SaveAsync();
            return _mapper.Map<GenderResponseDto>(gender);
        }

        public async Task<GenderResponseDto> DeleteGenderByIdAsync(long id)
        {
            var existGender = await _genderRepository.GetGenderByIdAsync(id);

            if (existGender is not null)
            {
                existGender.IsDeleted = true;
                existGender.UpdatedAt = DateTime.Now;

                existGender.Users
                    .ToList()
                    .ForEach(u =>
                    {
                        u.IsDeleted = true;
                        u.UpdatedAt = DateTime.Now;
                    });

                await _genderRepository.SaveAsync();
            }

            return _mapper.Map<GenderResponseDto>(existGender);
        }

        public async Task<IEnumerable<GenderResponseDto>> GetAllGendersAsync()
        {
            var genders = await _genderRepository.GetAllGenders().ToListAsync();
            return _mapper.Map<IEnumerable<GenderResponseDto>>(genders);
        }

        public async Task<GenderResponseDto> GetGenderByIdAsync(long id)
        {
            var existGender = await _genderRepository.GetGenderByIdAsync(id);
            return _mapper.Map<GenderResponseDto>(existGender);
        }

        public async Task<GenderResponseDto> GetGenderByNameAsync(string name, long? id = null)
        {
            Gender? existGenderByName;

            if (id is not null)
                existGenderByName = await _genderRepository.GetGenderByNameAsync(name, id);
            else
                existGenderByName = await _genderRepository.GetGenderByNameAsync(name);

            return _mapper.Map<GenderResponseDto>(existGenderByName);
        }

        public async Task<GenderResponseDto> UpdateGenderAsync(long id, GenderUpdateDto dto)
        {
            var existGender = await _genderRepository.GetByIdAsync(id);

            if (existGender is not null)
            {
                existGender.Name = dto.Name;

                await _genderRepository.UpdateAsync(existGender);
                await _genderRepository.SaveAsync();
            }

            return _mapper.Map<GenderResponseDto>(existGender);
        }
    }
}
