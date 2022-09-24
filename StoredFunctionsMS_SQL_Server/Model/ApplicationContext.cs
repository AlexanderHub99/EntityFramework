
using Microsoft.EntityFrameworkCore;

namespace StoredFunctionsMS_SQL_Server_.Model
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Company> Companies { get; set; } = null!;

        public ApplicationContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=12345helloappdb;Trusted_Connection=True;");
        }
    }
}

// Проецирование хранимой функции на метод класса
// Второй подход предполагает определение в классе контекста метода, который проецируется на хранимую функцию и через
// который можно вызывать данную функцию.
// Например, выше была определена хранимая табличная функция GetUsersByAge, которая в качестве параметра принимает
// некоторое число - возраст и возвращает набор пользователей (по сути набор объектов User). Создадим для этой функции
// метод. Для этого изменим класс контекста:
namespace StoredFunctionsMS_SQL_Server_.Model.ChangedСontextСlass
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Company> Companies { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public IQueryable<User> GetUsersByAge(int age) => FromExpression(() => GetUsersByAge(age));
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=12345helloappdb;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDbFunction(() => GetUsersByAge(default));
        }
    }
}