using Microsoft.EntityFrameworkCore;
using ShopwareX.Entities;
using ShopwareX.Repositories.Abstracts;
using ShopwareX.Services.Abstracts;

namespace ShopwareX.Services.Concretes
{
    public class GenderService(IGenderRepository genderRepository, IUserRepository userRepository) 
        : IGenderService
    {
        private readonly IGenderRepository _genderRepository = genderRepository;
        private readonly IUserRepository _userRepository = userRepository;

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
                await _genderRepository.DeleteByIdAsync(id);

                existGender.Users
                    .ToList()
                    .ForEach(async u =>
                    {
                        await _userRepository.DeleteByIdAsync(u.Id);
                        await _userRepository.SaveAsync();
                    });

                await _genderRepository.SaveAsync();
            }

            return existGender;
        }

        public async Task<IEnumerable<Gender>> GetAllGendersAsync()
            => await _genderRepository.GetAll().ToListAsync();

        public async Task<Gender?> GetGenderByIdAsync(long id)
            => await _genderRepository.GetByIdAsync(id);

        public async Task<Gender?> GetGenderByNameAsync(string name, long? id = null)
        {
            if (id is not null)
                return await _genderRepository.GetGenderByNameAsync(name, id);

            return await _genderRepository.GetGenderByNameAsync(name);
        }

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
