using ShopwareX.Entities;

namespace ShopwareX.Services.Abstracts
{
    public interface IRoleService
    {
        Task<Role> AddRoleAsync(Role role);
        Task<Role?> GetRoleByIdAsync(long id);
        Task<Role?> GetRoleWithUsersAsync(long id);
        Task<IEnumerable<Role>> GetAllRolesAsync();
        Task<Role?> UpdateRoleAsync(long id, Role role);
        Task<Role?> DeleteRoleByIdAsync(long id);
    }
}
