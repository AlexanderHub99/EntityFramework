namespace DatabaseSchemaManagementAndMigration.Model
{
    internal class User
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public int Age { get; set; }

        public string? Position { get; set; }   // Новое свойство - должность пользователя

        public bool IsMarried { get; set; }

        public long CarId { get; set; }

        public Car? Car { get; set; }
    }
}
