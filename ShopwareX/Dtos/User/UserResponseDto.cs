namespace ShopwareX.Dtos.User
{
    public class UserResponseDto
    {
        public long Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateOnly? DateOfBirth { get; set; }
        public long GenderId { get; set; }
        public string GenderName { get; set; } = null!;
        public long RoleId { get; set; }
        public string RoleName { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
