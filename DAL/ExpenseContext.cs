using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class ExpenseContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-I9IMOIH;Database=ExpenseDB;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True;");
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
