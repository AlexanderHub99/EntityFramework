using System.Data.Entity;

namespace EntityFramework.Models
{
    internal class UserContext : DbContext
    {
        public UserContext(): base("DbConnection"){ }

        public DbSet<User> Users { get; set; }
    }
}
