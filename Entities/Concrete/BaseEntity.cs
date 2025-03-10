namespace Entities.Concrete
{
    public class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public bool IsActive { get; set; } = true;

        public DateTime CreateDate { get; set; }=DateTime.Now;

        public DateTime UpdateDate { get; set; }
    }
}
