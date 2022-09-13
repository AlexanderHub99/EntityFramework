using Microsoft.EntityFrameworkCore;

namespace ConnectionСonfiguration.Model
{
    internal class User2Context : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;

        private readonly string connectionString;

        /// <summary>
        /// Установка конфигурации в конструкторе
        /// </summary>
        public User2Context(string connectionString)
        {
            this.connectionString = connectionString;   // получаем извне строку подключения
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(connectionString);
        }
    }
}
