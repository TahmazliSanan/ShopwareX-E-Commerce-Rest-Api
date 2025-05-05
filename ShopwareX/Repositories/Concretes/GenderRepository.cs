using ShopwareX.DataContext;
using ShopwareX.Entities;
using ShopwareX.Repositories.Abstracts;

namespace ShopwareX.Repositories.Concretes
{
    public class GenderRepository(AppDbContext context) 
        : GenericRepository<Gender>(context), IGenderRepository
    {
    }
}
