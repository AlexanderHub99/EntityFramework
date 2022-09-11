namespace LazyLoading.Model
{
    public class Company
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        // Также навигационное свойство Users в классе Company и навигационное свойство Company
        // в классе User определены как виртуальные, то есть имеют модификатор virtual.
        public virtual List<User> Users { get; set; } = new();
    }
}
