namespace ModelConfiguration.Model
{
    public class Kindergarten
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public int? ChildId { get; set; }

        public Child? Childs { get; set; }
    }
}
