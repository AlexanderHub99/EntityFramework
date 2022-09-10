using System.ComponentModel.DataAnnotations;

namespace CascadingDelete.Model
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        //Такая же таблица создается, если навигационное свойство представляет nullable-тип, но оно определено как
        //обязательное, например, с помощью атрибута Required:
        [Required]
        public int CompanyId { get; set; } // Свойство являетья обезательным так как не принемает нулевое значение
                                           // и будет созданна коскадная таблица 
        public Company? Company { get; set; }
    }

    // Если мы запустим программу с шаблонами обектов таблиц представленными ниже то при удалее нии обекта Company
    // объект User не будет удаляться так какон поддерживает нулевое значение в поле CompanyId
    public class User2
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        //Теперь внешний ключ имеет тип Nullable<int>, то есть он допускает значение null. Когда пользователь не
        //будет принадлежать ни одной компании,это свойство будет иметь значение null. И в этом случае скрипт
        //таблицы Users будет выглядеть следующим образом:
        public int? CompanyId { get; set; }      // необязательность наличия объекта Company:
        public Company? Company { get; set; }
    }

    // Аналогичная связь будет устанавливаться, если свойство-внешний ключа отсутствует, а навигационное
    // свойство представляет nullable-тип:
    public class User3
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public Company? Company { get; set; }
    }
}
