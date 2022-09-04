namespace DatabaseSchemaManagementAndMigration.Model
{
    internal class Car
    {
        public long Id { get; set; }

        public string? Name { get; set; }

        // public int Power { get; set; }

        public long UserId { get; set; }

        public User? User { get; set; }
    }
}
