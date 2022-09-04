using System.ComponentModel.DataAnnotations.Schema;

namespace CreatingModels.Model
{
    internal class User
    {
        [Column("user_Id")]
        public long Id { get; set; }

        [Column("user_Name")]
        public string? Name { get; set; }

        [Column("user_Age")]
        public int Age { get; set; }

        public long CompanyId { get; set; }

        public Company? Company { get; set; }
    }
}
