namespace Entities.Concrete
{
    public class Category:BaseEntity
    {
        public string CategoryName { get; set; }

        public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
