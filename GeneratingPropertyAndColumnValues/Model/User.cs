
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeneratingPropertyAndColumnValues.Model
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]// Атрибут DatabaseGeneratedAttribute представляет аннотацию,
                                                         // которая позволяет изменить поведение базы данных при
                                                         // добавлении или изменении.
                                                         // И если теперь мы попробуем добавить объект без установленного
                                                         // Id, то EF в качесте временного значения будет использовать
                                                         // значение по умолчанию, то есть Id=0. В итоге при добавление
                                                         // более одного объекта в бд мы получим ошибку:
                                                         // Если мы хотим, чтобы база данных, наоборот, сама генерировала
                                                         // значение, то в атрибут надо передавать значение
                                                         // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
                                                         // Но в данном случае для свойства Id это значение избыточно,
                                                         // так как значение генерируется по умолчанию.
        public int Id { get; set; }

        // Ограничение максимальной длины применяется только к строкам и к массивам, например, byte[].
        // Стоит отметить, что данное ограничение будет действовать только для тех систем баз данных, которые
        // поддерживают данную возможность. Например, для бд SQLite это не будет иметь никакого значения. А в случае
        // с бд MS SQL Server столбец Name в базе данных будет иметь тип nvarchar(50) и тем самым иметь ограничение
        // по длине.
        [MaxLength(50)]
        public string? Name { get; set; }                // Name должно представлять объединение свойств FirstName и LastName.

        public int? Age { get; set; }

        public DateTime CreatedAt { get; set; }          // Для генерации значения этого свойства в базе данных можно
                                                         // вызывать специальные функции, которые применяются в той
                                                         // или иной СУБД. Например, в MS SQL Server/T-SQL это функция
                                                         // GETDATE(), в SQLite это функции DATETIME()/DATE() и т.д.
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
