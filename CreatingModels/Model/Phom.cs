// Для увеличения производительности поиска в базе данных применяются индексы. По умолчанию индекс создается для каждого
// свойства, которое используется в качестве внешнего ключа. Однако Entity Framework также позволяет создавать свои
// индексы.
// Для создания индекса можно использовать атрибут [Index]. Например: [Index("PhoneNumber")]
// Первый и обязательный параметр атрибута указывает на свойство (или набор свойств), с которым будет ассоциирован
// индекс. В данном случае это свойство PhoneNumber.
// Но также он может принимать набор свойств, для которых создается индекс. В этом случае названия свойств просто
// перечисляются через запятую:[Index("PhoneNumber", "Passport")].
// С помощью дополнительных параметров можно настроить уникальность и имя индекса:
// [Index("PhoneNumber", IsUnique = true, Name ="Phone_Index")]
using Microsoft.EntityFrameworkCore;

namespace CreatingModels.Model
{
    [Index("PhoneNumber", IsUnique = true, Name = "Phone_Index")] // В данном случае индекс будет называться
                                                                  // Phone_Index, а значение IsUnique = true
                                                                  // указывает, что индекс должен быть уникальным.
    internal class Phone
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Passport { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
