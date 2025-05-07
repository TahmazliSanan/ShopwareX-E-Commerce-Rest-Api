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

        public async Task<Role?> GetRoleWithUsersAsync(long id)
            => await _roles
                .Include(g => g.Users)
                .FirstOrDefaultAsync(g => g.Id == id && g.IsDeleted == false);
    }
}
