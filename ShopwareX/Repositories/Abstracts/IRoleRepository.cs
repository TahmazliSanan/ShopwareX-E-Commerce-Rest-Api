using ShopwareX.Entities;

namespace ShopwareX.Repositories.Abstracts
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        Task<Role?> GetRoleByNameAsync(string name, long? id = null);
        Task<Role?> GetRoleWithUsersAsync(long id);
    }
}
