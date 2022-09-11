namespace LoadingRelatedDataMethod_Include.Model
{
    public class Company3
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int CountryId { get; set; }
        public Country2? Country { get; set; }
        public List<User3> Users3 { get; set; } = new();
    }
}