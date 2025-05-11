using ShopwareX.Entities;

namespace ShopwareX.Repositories.Abstracts
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        IQueryable<Role> GetAllRoles();
        Task<Role?> GetRoleByIdAsync(long id);
        Task<Role?> GetRoleByNameAsync(string name, long? id = null);
    }
}
