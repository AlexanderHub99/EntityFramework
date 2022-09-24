using Microsoft.EntityFrameworkCore;

namespace ObjectTrackingAndAsNoTracking.Model
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;

        public int UserAge { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=helloapp123.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //В метод HasQueryFilter() передается предикат, которому должен удовлетворять объект User, чтобы быть
            //извлеченным из базы данных. То есть в результате запросов будут извлекаться только те объекты User,
            //у которых значение свойства Age больше 17, а свойство RoleId равно значению свойства RoleId их
            //контекста данных.
            modelBuilder.Entity<User>().HasQueryFilter(u => u.Age > 17 && u.Age == UserAge);
        }
    }
}
