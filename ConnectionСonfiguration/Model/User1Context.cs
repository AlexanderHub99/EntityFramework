using Microsoft.EntityFrameworkCore;

namespace ConnectionСonfiguration.Model
{
    internal class User1Context : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public User1Context() => Database.EnsureCreated();

        /// <summary>
        /// Переопределение у класса контекста данных метода OnConfiguring()
        /// </summary>
        /// <param name="optionsBuilder">
        /// В этот метод передается объект класса DbContextOptionsBuilder,
        /// который позволяет установить параметры подключения. Для их конфигурации параметров подключения
        /// у этого класса определено ряд методов в зависимости от того, какую именно систему баз данных 
        /// мы собираемся использовать. Например, для установки подключения к SQLite вызывается метод 
        /// UseSqlite(), в который передается строка подключения.
        /// </param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=TestUser2.db");
        }
    }
}
