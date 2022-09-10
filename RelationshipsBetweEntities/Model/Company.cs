namespace RelationshipsBetweEntities.Model
{
    public class Company
    {
        public int Id { get; set; }
        public string? Name { get; set; } // название компании

        public List<User> Users { get; set; } = new();
    }
}
