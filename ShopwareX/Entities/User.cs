namespace ShopwareX.Entities
{
    public class User : BaseEntity
    {
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string HashedPassword { get; set; } = null!;
        public DateOnly? DateOfBirth { get; set; }
        public long GenderId { get; set; }
        public Gender Gender { get; set; } = null!;
        public long RoleId { get; set; }
        public Role Role { get; set; } = null!;
        public ICollection<Order> Orders { get; set; } = [];
    }
}
