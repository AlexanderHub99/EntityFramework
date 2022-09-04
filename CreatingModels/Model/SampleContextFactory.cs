
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CreatingModels.Model
{
    internal class SampleContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
    {
        public ApplicationContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext >();

            // получаем конфигурацию из файла appsettings.json
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");

            var config = builder.Build();
            var connectionString = config.GetConnectionString("DefaultConnection");// получаем строку подключения

            var optionsBuilder1 = new DbContextOptionsBuilder<ApplicationContext >();
            // получаем строку подключения из файла appsettings.json
            var options1 = optionsBuilder1.UseSqlite(connectionString).Options;
            return new ApplicationContext(options1);
        }
    }
}
