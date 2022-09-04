using System.ComponentModel.DataAnnotations.Schema;

namespace CreatingModels.Model
{
    [NotMapped]
    internal class Country
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
