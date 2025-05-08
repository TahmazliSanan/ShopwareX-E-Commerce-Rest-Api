using Microsoft.EntityFrameworkCore;
using ShopwareX.Entities;
using ShopwareX.Repositories.Abstracts;
using ShopwareX.Services.Abstracts;

namespace ShopwareX.Services.Concretes
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<User> AddUserAsync(User user)
        {
            user.RoleId = 2;
            await _userRepository.AddAsync(user);
            await _userRepository.SaveAsync();
            return user;
        }

        public async Task<User?> DeleteUserByIdAsync(long id)
        {
            var existUser = await GetUserByIdAsync(id);

            if (existUser is not null)
            {
                await _userRepository.DeleteByIdAsync(id);
                await _userRepository.SaveAsync();
            }

            return existUser;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
            => await _userRepository.GetAll().ToListAsync();

        public async Task<User?> GetUserByEmailAsync(string email)
            => await _userRepository.GetUserByEmail(email);

        public async Task<User?> GetUserByIdAsync(long id)
            => await _userRepository.GetByIdAsync(id);

        public async Task<User?> UpdateUserAsync(long id, User user)
        {
            var existUser = await GetUserByIdAsync(id);

            if (existUser is not null)
            {
                existUser.FullName = user.FullName;
                existUser.DateOfBirth = user.DateOfBirth;
                existUser.GenderId = user.GenderId;

                await _userRepository.UpdateAsync(existUser);
                await _userRepository.SaveAsync();
            }

            return existUser;
        }
    }
}
