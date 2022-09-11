using Microsoft.EntityFrameworkCore;

namespace LazyLoading.Model
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Company> Companies { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                //Прежде всего в методе OnConfiguring у объекта DbContextOptionsBuilder вызывается метод
                //UseLazyLoadingProxies(), который делает доступной ленивую загрузку.
                .UseLazyLoadingProxies()        // подключение lazy loading
                .UseSqlite("Data Source=helloapp.db");
        }
    }
}
