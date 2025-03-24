using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class ExpenseContext:DbContext
    {
        public ExpenseContext(DbContextOptions<ExpenseContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(e => e.Id);
                entity.Property(u => u.Username).IsRequired().HasMaxLength(50);
                entity.Property(u=>u.Email).IsRequired().HasMaxLength(50);
                entity.Property(u=>u.PasswordHash).IsRequired();
                entity.Property(u=>u.PasswordSalt).IsRequired();

                entity.HasOne(u => u.Role)
                .WithMany(r => r.Users) // Bir rolün birden fazla kullanıcısı olabilir
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.NoAction);

            });

            modelBuilder.Entity<Expense>(entity =>
            {
                entity.ToTable("Expenses");
                entity.HasKey(e => e.Id);
                entity.Property(e=>e.Description).IsRequired().HasMaxLength(150);
                entity.Property(e=>e.Amount).HasColumnType("decimal(18,2)").IsRequired();
                entity.Property(e=>e.ExpenseDate).IsRequired();
                
                entity.HasOne(u=>u.User)
                .WithMany(e=>e.Expenses)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(e => e.Category)
                .WithMany(c => c.Expenses).HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);
                
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Categories");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.CategoryName).IsRequired().HasMaxLength(50);

                entity.HasOne(c => c.User)
                .WithMany(u => u.Categories)
                .HasForeignKey(c => c.UserId).
                OnDelete(DeleteBehavior.Cascade);

            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Roles");
                entity.HasKey(r => r.Id);

                entity.Property(r => r.RoleName).IsRequired().HasMaxLength(50);
            });
        }
    }
}
