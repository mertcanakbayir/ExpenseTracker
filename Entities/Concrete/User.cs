namespace Entities.Concrete
{
    public class User:BaseEntity
    {
        public string Username { get; set; }
        public string Email { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public Guid RoleId { get; set; }

        public Role Role { get; set; }

        public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
    }
}
