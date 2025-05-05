using ShopwareX.Entities;
using ShopwareX.Repositories.Abstracts;
using ShopwareX.Services.Abstracts;

namespace ShopwareX.Services.Concretes
{
    public class GenderService(IGenderRepository genderRepository) : IGenderService
    {
        private readonly IGenderRepository _genderRepository = genderRepository;

        public async Task<Gender> AddAsync(Gender gender)
        {
            await _genderRepository.AddAsync(gender);
            await _genderRepository.SaveAsync();
            return gender;
        }

        public async Task<Gender?> DeleteAsync(long id)
        {
            var existGender = await _genderRepository.GetByIdAsync(id);
            await _genderRepository.DeleteAsync(id);
            return existGender;
        }

        public IQueryable<Gender> GetAll() => _genderRepository.GetAll();

        public async Task<Gender?> GetByIdAsync(long id) => await _genderRepository.GetByIdAsync(id);

        public async Task<Gender?> UpdateAsync(long id, Gender gender)
        {
            var existGender = await _genderRepository
                .GetByIdAsync(id)
                ?? throw new Exception("Gender not found");
            
            var newGender = new Gender
            {
                Name = existGender.Name
            };

            await _genderRepository.UpdateAsync(newGender);
            await _genderRepository.SaveAsync();
            return existGender;
        }
    }
}
