using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopwareX.Dtos.User;
using ShopwareX.Entities;
using ShopwareX.Repositories.Abstracts;
using ShopwareX.Services.Abstracts;

namespace ShopwareX.Services.Concretes
{
    public class UserService(IMapper mapper, IUserRepository userRepository) : IUserService
    {
        private readonly IMapper _mapper = mapper;
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<UserResponseDto> AddUserAsync(UserCreateDto dto)
        {
            var user = _mapper.Map<User>(dto);
            user.RoleId = 3;
            user.HashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            await _userRepository.AddAsync(user);
            await _userRepository.SaveAsync();

            return _mapper.Map<UserResponseDto>(user);
        }

        public async Task<UserResponseDto> DeleteUserByIdAsync(long id)
        {
            var existUser = await _userRepository.GetByIdAsync(id);

            if (existUser is not null)
            {
                await _userRepository.DeleteByIdAsync(id);
                await _userRepository.SaveAsync();
            }

            return _mapper.Map<UserResponseDto>(existUser);
        }

        public async Task<IEnumerable<UserResponseDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAll().ToListAsync();
            return _mapper.Map<IEnumerable<UserResponseDto>>(users);
        }

        public async Task<UserResponseDto> GetUserByEmailAsync(string email)
        {
            var existUserByEmail = await _userRepository.GetUserByEmailAsync(email);
            return _mapper.Map<UserResponseDto>(existUserByEmail);
        }

        public async Task<UserResponseDto> GetUserByIdAsync(long id)
        {
            var existUser = await _userRepository.GetUserByIdAsync(id);
            return _mapper.Map<UserResponseDto>(existUser);
        }

        public async Task<UserResponseDto> UpdateUserAsync(long id, UserUpdateDto dto)
        {
            var existUser = await _userRepository.GetByIdAsync(id);

            if (existUser is not null)
            {
                existUser.FullName = dto.FullName;
                existUser.DateOfBirth = dto.DateOfBirth;
                existUser.GenderId = dto.GenderId;

                await _userRepository.UpdateAsync(existUser);
                await _userRepository.SaveAsync();
            }

            return _mapper.Map<UserResponseDto>(existUser);
        }
    }
}
