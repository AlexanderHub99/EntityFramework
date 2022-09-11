
namespace LoadingRelatedDataMethod_Include.Model
{
    public class Country2
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int CapitalId { get; set; }
        public City? Capital { get; set; }  // столица страны
        public List<Company3> Companies { get; set; } = new();
    }
}
