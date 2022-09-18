using Microsoft.EntityFrameworkCore;

namespace ComplexTypes.Model
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;

        public ApplicationContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = helloapp.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Настройки связи можно использовать Fluent API, в частности, метод OwnsOne()
            modelBuilder.Entity<User>().OwnsOne(typeof(UserProfile), "Profile");
        }
    }
}
