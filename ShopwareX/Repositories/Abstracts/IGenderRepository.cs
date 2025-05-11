using ShopwareX.Entities;

namespace ShopwareX.Repositories.Abstracts
{
    public interface IGenderRepository : IGenericRepository<Gender>
    {
        IQueryable<Gender> GetAllGenders();
        Task<Gender?> GetGenderByIdAsync(long id);
        Task<Gender?> GetGenderByNameAsync(string name, long? id = null);
    }
}
