namespace LazyLoading.Model
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public int? CompanyId { get; set; }
        // Также навигационное свойство Users в классе Company и навигационное свойство Company
        // в классе User определены как виртуальные, то есть имеют модификатор virtual.
        public virtual Company? Company { get; set; }
    }
}