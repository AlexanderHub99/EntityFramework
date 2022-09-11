
namespace LoadingRelatedDataMethod_Include.Model
{
    public class Country
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public List<Company2> Companys2 { get; set; } = new List<Company2>();
    }
}