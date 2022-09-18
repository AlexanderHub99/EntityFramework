
namespace HierarchicalData.Model
{
    public class MenuItem
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public int? ParentId { get; set; }

        public MenuItem? Parent { get; set; }

        public List<MenuItem> Children { get; set; } = new List<MenuItem>();
    }
}
