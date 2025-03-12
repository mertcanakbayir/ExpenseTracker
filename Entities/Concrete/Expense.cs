namespace Entities.Concrete
{
    public class Expense:BaseEntity
    {
        public string Description { get; set; }

        public decimal Amount { get; set; }

        public DateTime ExpenseDate { get; set; }

        public Guid CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public Guid UserId { get; set; }

        public virtual User User { get; set; }

    }
}
