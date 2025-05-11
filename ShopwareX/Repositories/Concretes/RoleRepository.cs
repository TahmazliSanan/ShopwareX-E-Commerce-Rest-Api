using Microsoft.EntityFrameworkCore;
using ShopwareX.DataContext;
using ShopwareX.Entities;
using ShopwareX.Repositories.Abstracts;

namespace ShopwareX.Repositories.Concretes
{
    public class RoleRepository(AppDbContext context)
        : GenericRepository<Role>(context), IRoleRepository
    {
        private readonly DbSet<Role> _roles = context.Set<Role>();

        public IQueryable<Role> GetAllRoles()
            => _roles
            .Include(r => r.Users)
            .AsQueryable();

        public async Task<Role?> GetRoleByIdAsync(long id)
            => await _roles
                .Include(g => g.Users)
                .FirstOrDefaultAsync(g => g.Id == id && g.IsDeleted == false);

        public async Task<Role?> GetRoleByNameAsync(string name, long? id = null)
        {
            if (id is not null)
                return await _roles
                .FirstOrDefaultAsync(g => g.Name.Trim().ToLower()
                .Equals(name.Trim().ToLower()) && g.Id != id && g.IsDeleted == false);

            return await _roles
                .FirstOrDefaultAsync(g => g.Name.Trim().ToLower()
                .Equals(name.Trim().ToLower()) && g.IsDeleted == false);
        }
    }
}
