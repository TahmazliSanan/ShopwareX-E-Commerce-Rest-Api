using ShopwareX.Dtos.Login;

namespace ShopwareX.Services.Abstracts
{
    public interface IAuthService
    {
        Task<LoginResponseDto?> LoginUserAsync(LoginRequestDto dto);
    }
}
