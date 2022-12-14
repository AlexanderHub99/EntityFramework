using System.ComponentModel.DataAnnotations.Schema;

namespace LoggingOperations.Model
{
    internal class User
    {
        [Column("user_Id")]
        public long Id { get; set; }

        [Column("user_Name")]
        public string? Name { get; set; }

        [Column("user_Age")]
        public int Age { get; set; }
    }
}
