using ShopwareX.Entities;

namespace ShopwareX.Services.Abstracts
{
    public interface IJwtGenerator
    {
        string GenerateJwt(User user);
    }
}
