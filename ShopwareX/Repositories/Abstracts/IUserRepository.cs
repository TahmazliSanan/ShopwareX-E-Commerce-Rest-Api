using ShopwareX.Entities;

namespace ShopwareX.Repositories.Abstracts
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetUserByIdAsync(long id);
        Task<User?> GetUserByEmailAsync(string email);
    }
}
