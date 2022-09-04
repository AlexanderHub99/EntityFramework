using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CreatingModels.Model
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;

        //Создаем файл для записи логов 
        private readonly StreamWriter logStream = new StreamWriter("mylog.txt", true);

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // использование Fluent API
            base.OnModelCreating(modelBuilder);
            //Один из способов сопоставления модели с базой данных
            modelBuilder.Ignore<Company>();

            //Игнорирует создание столбца  в дб
            modelBuilder.Entity<User>().Ignore(u => u.Age);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Логгирование в окно Output
            optionsBuilder.LogTo(message => System.Diagnostics.Debug.WriteLine(message), LogLevel.Information);
            //Записывает логи в (mylog.txt) Храниться в корневой папке проекта CodeFirst\LoggingOperations\bin\Debug\net6.0
            optionsBuilder.LogTo(logStream.WriteLine);
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
