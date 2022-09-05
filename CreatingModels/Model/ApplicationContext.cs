using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CreatingModels.Model
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;

        // Создаем файл для записи логов 
        private readonly StreamWriter logStream = new StreamWriter("mylog.txt", true);

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);               // Использование Fluent API
            modelBuilder.Ignore<Company>();                   // Один из способов сопоставления модели с базой данных

            modelBuilder.Entity<User>()                       // Игнорирует создание столбца  в дб
                .Ignore(u => u.Age);

            modelBuilder.Entity<User>()                       // Указывает, что данное свойство обязательно для установки,
                .Property(b => b.Name).IsRequired();          // то есть будет иметь определение NOT NULL в БД,
                                                              // Даже если оно представляет nullable-тип:

            modelBuilder.Entity<User>()                       // Для конфигурации ключа с Fluent API применяется метод
                .HasKey(u => u.Id);                           // HasKey(): устанавливает поле как ключ в Базе Данных

            modelBuilder.Entity<User>()                       // Дополнительно с помощью Fluent API можно настроить имя
                .HasKey(u => u.Id).HasName("UsersPrimaryKey");// ограничения, которое задается для первичного ключа.
                                                              // Для этого применяется метод HasName():

            modelBuilder.Entity<User>()                       // С помощью Fluent API можно создать составной ключ из
                .HasKey(u => new { u.PassportSeria, u.PassportNumber });// нескольких свойств:

            modelBuilder.Entity<Company>()                     // Для установки альтернативного ключа используется метод
                .HasAlternateKey(u => u.Passport);             // HasAlternateKey():

            modelBuilder.Entity<Company>()                     // Альтернативные ключи также могут быть составными:
                .HasAlternateKey(u => new { u.Passport, u.Name });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Логгирование в окно Output
            optionsBuilder.LogTo(message => System.Diagnostics.Debug.WriteLine(message), LogLevel.Information);

            optionsBuilder.LogTo(logStream.WriteLine);         // Записывает логи в (mylog.txt) Храниться в корневой папке
                                                               // проекта CodeFirst\LoggingOperations\bin\Debug\net6.0
        }

        /// <summary>
        /// Для закрытия и утилизации файлового потока StreamWriter переопределены методы
        /// Dispose/DisposeAsync, в которых вызывается метод Dispose/DisposeAsync объекта StreamWriter. 
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();
            logStream.Dispose();
        }

        /// <summary>
        /// Для закрытия и утилизации файлового потока StreamWriter переопределены методы
        /// Dispose/DisposeAsync, в которых вызывается метод Dispose/DisposeAsync объекта StreamWriter. 
        /// </summary>
        public override async ValueTask DisposeAsync()
        {
            await base.DisposeAsync();
            await logStream.DisposeAsync();
        }
    }
}
