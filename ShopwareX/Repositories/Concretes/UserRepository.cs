using Microsoft.EntityFrameworkCore;
using ShopwareX.DataContext;
using ShopwareX.Entities;
using ShopwareX.Repositories.Abstracts;

namespace ShopwareX.Repositories.Concretes
{
    public class UserRepository(AppDbContext context)
        : GenericRepository<User>(context), IUserRepository
    {
        private readonly DbSet<User> _users = context.Set<User>();

        public async Task<User?> GetUserByEmailAsync(string email)
            => await _users
            .Include(u => u.Role)
            .Include(u => u.Gender)
            .FirstOrDefaultAsync(u => u.Email.Trim().ToLower()
            .Equals(email.Trim().ToLower()) && u.IsDeleted == false);

        public async Task<User?> GetUserByIdAsync(long id)
            => await _users
            .Include(u => u.Gender)
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Id == id && u.IsDeleted == false);
    }
}
