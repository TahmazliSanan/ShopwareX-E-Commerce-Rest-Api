using ShopwareX.Entities;

namespace ShopwareX.Repositories.Abstracts
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetUserByEmailAsync(string email);
    }
}
