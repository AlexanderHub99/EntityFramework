using Microsoft.EntityFrameworkCore;

namespace TPT_Table_Per_Type.Mobel
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;

        public DbSet<Employee> Employees { get; set; } = null!;

        public DbSet<Manager> Managers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=helloapp.db");
        }
    }
}
