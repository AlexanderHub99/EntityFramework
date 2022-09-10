
using Microsoft.EntityFrameworkCore;

namespace RelationshipsBetweEntities.Model
{
    public class ApplicationContext : DbContext
    {
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Настройка ключа с помощью Fluent API
            // Для настройки отношений между моделями с помощью Fluent API применяются специальные методы: HasOne / HasMany
            // / WithOne / WithMany. Методы HasOne и HasMany устанавливают навигационное свойство для сущности, для которой
            // производится конфигурация.Далее могут идти вызовы методов WithOne и WithMany, который идентифицируют
            // навигационное свойство на стороне связанной сущности. Методы HasOne/WithOne применяются для обычного
            // навигационного свойства, представляющего одиночный объект, а методы HasMany/WithMany используются для
            // навигационных свойств, представляющих коллекции. Сам же внешний ключ устанавливается с помощью метода
            // HasForeignKey:
            modelBuilder.Entity<User>()// в объекте User
                    .HasOne(u => u.Company)  // есть поле Company (ссылка на объект Company)
                     .WithMany(c => c.Users)  // которая может принадлежать объектом User
                       .HasForeignKey(u => u.CompanyInfoKey);// По ключу CompanyInfoKey
        }
    }
}
