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

            modelBuilder.Entity<Phone>()                       // Для создания индекса через Fluent API применяется
                .HasIndex(u => u.Passport);                    // метод HasIndex():

            modelBuilder.Entity<Phone>()                       // С помощью дополнительного метода IsUnique() можно
                 .HasIndex(u => u.Passport).IsUnique();        // указать, что индекс должен иметь уникальное значение.
                                                               // Тем самым мы гарантируем, что в базе данных может
                                                               // быть только один объект с определенным значением
                                                               // для свойства-индекса:

            modelBuilder.Entity<Phone>()                       // Также можно определить индексы сразу для нескольких свойств:
                .HasIndex(u => new { u.Passport, u.PhoneNumber });

            modelBuilder.Entity<Phone>()
                .HasIndex(u => u.PhoneNumber)                  // Для установки имени индекса применяется метод 
                .HasDatabaseName("PhoneIndex");           // HasDatabaseName(), в который передается имя индекса:

            modelBuilder.Entity<Phone>()                       // Некоторые системы управления базами данных позволяют
                .HasIndex(u => u.PhoneNumber)                  // определять индексы с фильрами или частичные индексы,
                .HasFilter("[PhoneNumber] IS NOT NULL");   // которые позволяют выполнять индексацию только по
                                                           // ограниченному набору значений, что увеличивает
                                                           // производительность и уменьшает использование дискового
                                                           // простанства. И EntityFramework Core также позволяет
                                                           // создавать подобные индексы. Для этого применяется метод
                                                           // HasFilter(), в который передается sql-выражение, которое
                                                           // определяет условие фильтра. Например:

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
