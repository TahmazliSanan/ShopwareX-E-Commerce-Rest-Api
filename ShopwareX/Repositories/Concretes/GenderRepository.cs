using Microsoft.EntityFrameworkCore;
using ShopwareX.DataContext;
using ShopwareX.Entities;
using ShopwareX.Repositories.Abstracts;

namespace ShopwareX.Repositories.Concretes
{
    public class GenderRepository(AppDbContext context)
        : GenericRepository<Gender>(context), IGenderRepository
    {
        private readonly DbSet<Gender> _genders = context.Set<Gender>();

        public async Task<Gender?> GetGenderByNameAsync(string name, long? id = null)
        {
            if (id is not null)
                return await _genders
                .FirstOrDefaultAsync(g => g.Name.Trim().ToLower()
                .Equals(name.Trim().ToLower()) && g.Id != id && g.IsDeleted == false);

            return await _genders
                .FirstOrDefaultAsync(g => g.Name.Trim().ToLower()
                .Equals(name.Trim().ToLower()) && g.IsDeleted == false);
        }

        public async Task<Gender?> GetGenderWithUsersAsync(long id)
            => await _genders
            .Include(g => g.Users)
            .FirstOrDefaultAsync(g => g.Id == id && g.IsDeleted == false);
    }
}
