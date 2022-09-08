using GeneratingPropertyAndColumnValues.Model;
using Microsoft.EntityFrameworkCore;

public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
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
        modelBuilder.Entity<User>()                        // Отключение автогенерации значения для свойства с помощью
            .Property(b => b.Id)                           // Fluent API:
            .ValueGeneratedNever();

        modelBuilder.Entity<User>()                        // Для свойств, которые не представляют ключи и для которых
             .Property(u => u.Age)                         // не устанавливается значения, используются значения по
             .HasDefaultValue(18);                   // умолчанию. Например, для свойств типа int это значение
                                                     // 0. С помощью метода HasDefaultValue() можно переопределить
                                                     // значение по умолчанию, которое будет применяться после
                                                     // добавления объекта в базу данных:

        // В метод HasDefaultValueSql() передается SQL-выражение, которые вызывается при добавлении объекта User в базу
        // данных. Поскольку в данном случае используется база данных SQLite, то в качестве SQL-выражения передается
        // вызов функции DATETIME('now') - "now" здесь указывает, что мы хотим получить текущую дату.
        modelBuilder.Entity<User>()
            .Property(u => u.CreatedAt)
            .HasDefaultValueSql("DATETIME('now')");

        // HasComputedColumnSql() можно установить в бд SQL - выражение, которое будет устанавливать значение столбца Name
        // на основании столбцов FirstName и LastName:
        modelBuilder.Entity<User>()
               .Property(u => u.Name)
               .HasComputedColumnSql("FirstName || ' ' || LastName");

        // В метод HasCheckConstraint() передается название столбца и sql-выражение, которое выполняет проверку
        // корректности передаваемого значения. Например, у пользователя есть возраст, который должен укладываться в
        // некоторые допустимые рамки. К примеру, возраст не может быть меньше 0 и больше 120:
        modelBuilder.Entity<User>()
            .HasCheckConstraint("Age", "Age > 0 AND Age < 120");

        // В Fluent API ограничение по длине устанавливается с помощью метода HasMaxLength():
        modelBuilder.Entity<User>()
            .Property(b => b.Name)
            .HasMaxLength(50);
    }
}
