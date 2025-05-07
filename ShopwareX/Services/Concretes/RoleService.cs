using Microsoft.EntityFrameworkCore;
using ShopwareX.Entities;
using ShopwareX.Repositories.Abstracts;
using ShopwareX.Services.Abstracts;

namespace ShopwareX.Services.Concretes
{
    public class RoleService(IRoleRepository roleRepository) : IRoleService
    {
        private readonly IRoleRepository _roleRepository = roleRepository;

        public async Task<Role> AddRoleAsync(Role role)
        {
            await _roleRepository.AddAsync(role);
            await _roleRepository.SaveAsync();
            return role;
        }

        public async Task<Role?> DeleteRoleByIdAsync(long id)
        {
            var existRole = await GetRoleWithUsersAsync(id);

            if (existRole is not null)
            {
                existRole.IsDeleted = true;
                existRole.UpdatedAt = DateTime.UtcNow;

                existRole.Users
                    .ToList()
                    .ForEach(u =>
                    {
                        u.IsDeleted = true;
                        u.UpdatedAt = DateTime.Now;
                    });

                await _roleRepository.SaveAsync();
            }

            return existRole;
        }

        public async Task<IEnumerable<Role>> GetAllRolesAsync()
            => await _roleRepository.GetAll().ToListAsync();

        public async Task<Role?> GetRoleByIdAsync(long id)
            => await _roleRepository.GetByIdAsync(id);

        public async Task<Role?> GetRoleWithUsersAsync(long id)
            => await _roleRepository.GetRoleWithUsersAsync(id);

        public async Task<Role?> UpdateRoleAsync(long id, Role role)
        {
            var existRole = await GetRoleByIdAsync(id);

            if (existRole is not null)
            {
                existRole.Name = role.Name;

                await _roleRepository.UpdateAsync(existRole);
                await _roleRepository.SaveAsync();
            }

            return existRole;
        }
    }
}
