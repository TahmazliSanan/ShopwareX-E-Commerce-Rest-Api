using ShopwareX.Entities;

namespace ShopwareX.Services.Abstracts
{
    public interface IUserService
    {
        Task<User> AddUserAsync(User user);
        Task<User?> GetUserByIdAsync(long id);
        Task<User?> GetUserByEmailAsync(string email);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> UpdateUserAsync(long id, User user);
        Task<User?> DeleteUserByIdAsync(long id);
    }
}
