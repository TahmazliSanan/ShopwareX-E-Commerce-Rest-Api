using ShopwareX.Entities;

namespace ShopwareX.Repositories.Abstracts
{
    public interface IGenderRepository : IGenericRepository<Gender>
    {
        Task<Gender?> GetGenderByNameAsync(string name, long? id = null);
        Task<Gender?> GetGenderWithUsersAsync(long id);
    }
}
