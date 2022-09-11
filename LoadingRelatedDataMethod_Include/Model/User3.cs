namespace LoadingRelatedDataMethod_Include.Model
{
    public class User3
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? CompanyId { get; set; }
        public Company3? Company3 { get; set; }
        public int? PositionId { get; set; }
        public Position? Position { get; set; }
    }
}