using Microsoft.EntityFrameworkCore;
using ModelConfiguration.Model.EntityTypeConfig;

namespace ModelConfiguration.Model
{
    [EntityTypeConfiguration(typeof(UserConfiguration))]
    public class Child
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public int? KindergartenId { get; set; }

        public Kindergarten? Kindergartens { get; set; }


    }
}
