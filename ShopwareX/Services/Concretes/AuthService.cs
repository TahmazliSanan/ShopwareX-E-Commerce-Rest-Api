using ShopwareX.Dtos.Login;
using ShopwareX.Repositories.Abstracts;
using ShopwareX.Services.Abstracts;

namespace ShopwareX.Services.Concretes
{
    public class AuthService(IUserRepository userRepository, IJwtGenerator jwtGenerator) : IAuthService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IJwtGenerator _jwtGenerator = jwtGenerator;

        public async Task<LoginResponseDto?> LoginUserAsync(LoginRequestDto dto)
        {
            var existUserByEmail = await _userRepository.GetUserByEmailAsync(dto.Email);

            if (existUserByEmail is not null)
            {
                bool isMatched = BCrypt.Net.BCrypt
                    .Verify(dto.Password, existUserByEmail.HashedPassword);

                if (isMatched)
                {
                    var jwt = _jwtGenerator.GenerateJwt(existUserByEmail);

                    return new LoginResponseDto
                    {
                        Id = existUserByEmail.Id,
                        FullName = existUserByEmail.FullName,
                        Email = existUserByEmail.Email,
                        DateOfBirth = existUserByEmail.DateOfBirth,
                        RoleId = existUserByEmail.RoleId,
                        GenderId = existUserByEmail.GenderId,
                        Jwt = jwt
                    };
                }
            }

            return null;
        }
    }
}
