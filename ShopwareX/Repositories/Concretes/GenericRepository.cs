using Microsoft.EntityFrameworkCore;
using ShopwareX.DataContext;
using ShopwareX.Entities;
using ShopwareX.Repositories.Abstracts;

namespace ShopwareX.Repositories.Concretes
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly AppDbContext _context;
        private readonly DbSet<TEntity> _set;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _set = _context.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            entity.CreatedAt = DateTime.Now;
            await _set.AddAsync(entity);
        }

        public async Task DeleteByIdAsync(long id)
        {
            var existEntity = await _set
                .FirstOrDefaultAsync(e => e.Id == id && e.IsDeleted == false);

            if (existEntity is not null)
            {
                existEntity.UpdatedAt = DateTime.Now;
                existEntity.IsDeleted = true;
            }
        }

        public IQueryable<TEntity> GetAll() 
            => _set.Where(e => e.IsDeleted == false).AsQueryable();

        public async Task<TEntity?> GetByIdAsync(long id)
            => await _set.FirstOrDefaultAsync(e => e.Id == id && e.IsDeleted == false);

        public async Task SaveAsync()
            => await _context.SaveChangesAsync();

        public Task UpdateAsync(TEntity entity)
        {
            entity.UpdatedAt = DateTime.Now;
            _set.Update(entity);
            return Task.CompletedTask;
        }
    }
}
