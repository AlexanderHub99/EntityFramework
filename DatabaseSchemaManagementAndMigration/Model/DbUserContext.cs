using Microsoft.EntityFrameworkCore;

namespace DatabaseSchemaManagementAndMigration.Model
{
    internal class DbUserContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;

        public DbSet<Car> Car { get; set; } = null!;

        public DbUserContext(DbContextOptions<DbUserContext> options) : base(options)
        {
            /// Database.EnsureCreated();      Выхзывает ошибку при миграции 
            Database.Migrate();
            //Также стоит отметить, что при самом первом применении миграции по отношению к БД SQLite Entity Framework
            //пытается создать ее заново, однако если создаваемые таблицы в ней уже есть, то мы столкнемся с ошибкой.
            //Поэтому следует убедиться, что по используемому пути нет файла базы данных с подобным именем.
            //При последующих применениях миграции EF будет использовать бд, созданную при первой миграции.
        }

        //  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //  optionsBuilder.UseSqlite("Data Source=TestD21b.db");
        // }
    }
}
