namespace Core.DTOs
{
    public class ExpenseDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; } 
        public decimal Amount { get; set; }
        public DateTime ExpenseDate { get; set; }
        public Guid CategoryId { get; set; }
        public Guid UserId { get; set; }
    }
}
