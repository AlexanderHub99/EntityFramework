using Microsoft.EntityFrameworkCore;

namespace LoadingRelatedDataMethod_Include.Model
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<City> Cities { get; set; } = null!;
        public DbSet<User2> User2s { get; set; } = null!;
        public DbSet<User3> Users3 { get; set; } = null!;
        public DbSet<Country> Countries { get; set; } = null!;
        public DbSet<Company> Companies { get; set; } = null!;
        public DbSet<Position> Positions { get; set; } = null!;
        public DbSet<Company2> Companies2 { get; set; } = null!;
        public DbSet<Company3> Companies3 { get; set; } = null!;
        public DbSet<Country2> Countries2 { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlite("Data Source=Helloapp.db");
        }

    }
}
