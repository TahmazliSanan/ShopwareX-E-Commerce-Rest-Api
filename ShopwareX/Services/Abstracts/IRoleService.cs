using ShopwareX.Dtos.Role;

namespace ShopwareX.Services.Abstracts
{
    public interface IRoleService
    {
        Task<RoleResponseDto> AddRoleAsync(RoleCreateDto dto);
        Task<RoleResponseDto> GetRoleByIdAsync(long id);
        Task<RoleResponseDto> GetRoleByNameAsync(string name, long? id = null);
        Task<IEnumerable<RoleResponseDto>> GetAllRolesAsync();
        Task<RoleResponseDto> UpdateRoleAsync(long id, RoleUpdateDto dto);
        Task<RoleResponseDto> DeleteRoleByIdAsync(long id);
    }
}
