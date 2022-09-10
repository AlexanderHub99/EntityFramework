using System.ComponentModel.DataAnnotations.Schema;

namespace RelationshipsBetweEntities.Model
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public int CompanyInfoKey { get; set; }      // внешний ключ

        [ForeignKey("CompanyInfoKey")]          //Чтобы установить свойство в качестве внешнего ключа, применяется атрибут
                                                //[ForeignKey]:
        public Company? Company { get; set; }        // навигационное свойство
    }
}
