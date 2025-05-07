using ShopwareX.Entities;

namespace ShopwareX.Services.Abstracts
{
    public interface IGenderService
    {
        Task<Gender> AddGenderAsync(Gender gender);
        Task<Gender?> GetGenderByIdAsync(long id);
        Task<Gender?> GetGenderWithUsersAsync(long id);
        Task<IEnumerable<Gender>> GetAllGendersAsync();
        Task<Gender?> UpdateGenderAsync(long id, Gender gender);
        Task<Gender?> DeleteGenderByIdAsync(long id);
    }
}
