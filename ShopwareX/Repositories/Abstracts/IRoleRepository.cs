using ShopwareX.Entities;

namespace ShopwareX.Repositories.Abstracts
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        Task<Role?> GetRoleWithUsersAsync(long id);
    }
}
