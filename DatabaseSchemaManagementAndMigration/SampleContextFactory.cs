
using DatabaseSchemaManagementAndMigration.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DatabaseSchemaManagementAndMigration
{
    internal class SampleContextFactory : IDesignTimeDbContextFactory<DbUserContext>
    {
        public DbUserContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DbUserContext>();

            // получаем конфигурацию из файла appsettings.json
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");

            var config = builder.Build();
            var connectionString = config.GetConnectionString("DefaultConnection");// получаем строку подключения

            var optionsBuilder1 = new DbContextOptionsBuilder<DbUserContext>();
            // получаем строку подключения из файла appsettings.json
            var options1 = optionsBuilder1.UseSqlite(connectionString).Options;
            return new DbUserContext(options1);
        }
    }
}
