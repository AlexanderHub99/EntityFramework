
namespace LoadingRelatedDataMethod_Include.Model
{
    public class User2
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public int? Company2Id { get; set; }

        public Company2? Company2 { get; set; }
    }
}
