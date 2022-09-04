
using Microsoft.EntityFrameworkCore;

namespace ConnectionСonfiguration.Model
{
    internal class User3Context : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;

        /// <summary>
        /// Установка конфигурации в конструкторе
        /// </summary>
        public User3Context(DbContextOptions<User3Context> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
