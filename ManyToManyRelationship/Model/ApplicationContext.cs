using Microsoft.EntityFrameworkCore;

namespace ManyToManyRelationship.Model
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Course> Courses { get; set; } = null!;
        public DbSet<Student> Students { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=helloapp.db");
        }
    }
}
