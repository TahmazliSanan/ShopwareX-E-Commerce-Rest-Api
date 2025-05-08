namespace ShopwareX.Dtos.Login
{
    public class LoginResponseDto
    {
        public long Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateOnly? DateOfBirth { get; set; }
        public long GenderId { get; set; }
        public long RoleId { get; set; }
        public string Jwt { get; set; } = null!;
    }
}
