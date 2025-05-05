using ShopwareX.Entities;

namespace ShopwareX.Services.Abstracts
{
    public interface IGenderService
    {
        Task<Gender> AddAsync(Gender gender);
        Task<Gender?> GetByIdAsync(long id);
        Task<IEnumerable<Gender>> GetAllAsync();
        Task<Gender?> UpdateAsync(long id, Gender gender);
        Task<Gender?> DeleteAsync(long id);
    }
}
