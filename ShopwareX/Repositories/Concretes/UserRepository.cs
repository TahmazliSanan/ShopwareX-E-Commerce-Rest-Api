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

        public async Task<User?> GetUserByEmail(string email)
            => await _users.FirstOrDefaultAsync(u => u.Email.Trim().ToLower()
            .Equals(email.Trim().ToLower()) && u.IsDeleted == false);
    }
}
