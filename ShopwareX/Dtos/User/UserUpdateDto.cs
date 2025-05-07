namespace ShopwareX.Dtos.User
{
    public class UserUpdateDto
    {
        public string FullName { get; set; } = null!;
        public DateOnly? DateOfBirth { get; set; }
        public long GenderId { get; set; }
    }
}
