using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModelConfiguration.Model.EntityTypeConfig;

namespace ModelConfiguration.Model
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;

        public DbSet<Child> Childs { get; set; } = null!;

        public DbSet<Kindergarten> Kindergartens { get; set; } = null!;

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
        {   // конфигурация для типа User
            modelBuilder.Entity<User>().ToTable("People").Property(p => p.Name).IsRequired();
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            // конфигурация для типа Kindergarten
            modelBuilder.Entity<Kindergarten>(KindergartenConfigure);

            // конфигурация для типа Child в одноименной модели 
        }

        // конфигурация для типа Kindergarten
        public void KindergartenConfigure(EntityTypeBuilder<Kindergarten> builder)
        {
            builder.ToTable("Enterprises").Property(c => c.Name).IsRequired();
        }
    }
}
