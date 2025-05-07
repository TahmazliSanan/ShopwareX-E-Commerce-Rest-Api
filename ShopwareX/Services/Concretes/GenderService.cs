using Microsoft.EntityFrameworkCore;
using ShopwareX.Entities;
using ShopwareX.Repositories.Abstracts;
using ShopwareX.Services.Abstracts;

namespace ShopwareX.Services.Concretes
{
    public class GenderService(IGenderRepository genderRepository) : IGenderService
    {
        private readonly IGenderRepository _genderRepository = genderRepository;

        public async Task<Gender> AddGenderAsync(Gender gender)
        {
            await _genderRepository.AddAsync(gender);
            await _genderRepository.SaveAsync();
            return gender;
        }

        public async Task<Gender?> DeleteGenderByIdAsync(long id)
        {
            var existGender = await GetGenderWithUsersAsync(id);

            if (existGender is not null)
            {
                existGender.IsDeleted = true;
                existGender.UpdatedAt = DateTime.UtcNow;

                existGender.Users
                    .ToList()
                    .ForEach(u =>
                    {
                        u.IsDeleted = true;
                        u.UpdatedAt = DateTime.Now;
                    });

                await _genderRepository.SaveAsync();
            }

            return existGender;
        }

        public async Task<IEnumerable<Gender>> GetAllGendersAsync()
            => await _genderRepository.GetAll().ToListAsync();

        public async Task<Gender?> GetGenderByIdAsync(long id)
            => await _genderRepository.GetByIdAsync(id);

        public async Task<Gender?> GetGenderWithUsersAsync(long id)
            => await _genderRepository.GetGenderWithUsersAsync(id);

        public async Task<Gender?> UpdateGenderAsync(long id, Gender gender)
        {
            var existGender = await GetGenderByIdAsync(id);

            if (existGender is not null)
            {
                existGender.Name = gender.Name;

                await _genderRepository.UpdateAsync(existGender);
                await _genderRepository.SaveAsync();
            }
            
            return existGender;
        }
    }
}
