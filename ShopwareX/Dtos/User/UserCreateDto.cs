namespace ShopwareX.Dtos.User
{
    public class UserCreateDto
    {
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public long GenderId { get; set; }
    }
}
