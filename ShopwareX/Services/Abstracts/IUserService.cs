using ShopwareX.Dtos.User;

namespace ShopwareX.Services.Abstracts
{
    public interface IUserService
    {
        Task<UserResponseDto> AddUserAsync(UserCreateDto dto);
        Task<UserResponseDto> GetUserByIdAsync(long id);
        Task<UserResponseDto> GetUserByEmailAsync(string email);
        Task<IEnumerable<UserResponseDto>> GetAllUsersAsync();
        Task<UserResponseDto> UpdateUserAsync(long id, UserUpdateDto dto);
        Task<UserResponseDto> DeleteUserByIdAsync(long id);
    }
}
