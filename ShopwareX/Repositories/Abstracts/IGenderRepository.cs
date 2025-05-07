using ShopwareX.Entities;

namespace ShopwareX.Repositories.Abstracts
{
    public interface IGenderRepository : IGenericRepository<Gender>
    {
        Task<Gender?> GetGenderWithUsersAsync(long id);
    }
}
