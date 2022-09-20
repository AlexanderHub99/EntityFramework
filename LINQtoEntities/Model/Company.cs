
namespace LINQtoEntities.Model
{
    public class Company
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public int? CountryId { get; set; }      // int? для того чтобы объект   Country мог отсутствовать == null в таблице при ее заполнении 

        public Country? Country { get; set; }

        public List<User> Users { get; set; } = new();
    }
}
