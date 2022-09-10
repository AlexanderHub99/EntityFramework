using Microsoft.EntityFrameworkCore;

namespace CascadingDelete.Model
{
    internal class ApplicationContext : DbContext
    {
        // Здесь свойство внешнего ключа имеет тип int, оно не допускает значения null и требует наличия конкретного
        // значения - id связанного объекта Company (При этом то, что навигационное свойство Company допускает null,
        // не имеет значения). То есть для объекта User обязательно необходимо наличия связанного объекта Company.
        // Поэтому сгенерированная таблица Users будет иметь код:
        //  CREATE TABLE "Users" (
        // "Id"    INTEGER NOT NULL,
        // "Name"  TEXT,
        // "CompanyId" INTEGER NOT NULL,
        // CONSTRAINT "PK_Users" PRIMARY KEY("Id" AUTOINCREMENT),
        // CONSTRAINT "FK_Users_Companies_CompanyId" FOREIGN KEY("CompanyId") REFERENCES "Companies"("Id") ON DELETE CASCADE
        // );
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Company> Companies { get; set; } = null!;
        public ApplicationContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=helloapp.db");
        }

        // Настройка каскадного удаления с помощью Fluent API
        // В Fluent API доступны три разных сценария, которые управляют поведением зависимой сущности в случае удаления главной сущности:
        // Cascade: зависимая сущность удаляется вместе с главной
        // SetNull: свойство-внешний ключ в зависимой сущности получает значение null
        // Restrict: зависимая сущность никак не изменяется при удалении главной сущности
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.Company)
                .WithMany(c => c.Users)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
