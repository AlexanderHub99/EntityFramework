using Microsoft.EntityFrameworkCore;

namespace ConnectionСonfiguration.Model
{
    internal class User4Context : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public User4Context(DbContextOptions<User4Context> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
