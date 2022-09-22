using Microsoft.EntityFrameworkCore;
using ObjectTrackingAndAsNoTracking.Model;

Console.WriteLine("Hello, World!");

// Пусть в базе данных есть есть несколько объектов User:
using (ApplicationContext db = new ApplicationContext())
{
    // пересоздаем базу данных
    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();

    User tom = new User { Name = "Tom", Age = 36};
    User bob = new User { Name = "Bob", Age = 39 };
    User alice = new User { Name = "Alice", Age = 28 };
    User kate = new User { Name = "Kate", Age = 25 };

    db.Users.AddRange(tom, bob, alice, kate);
    db.SaveChanges();
}

// При обычном выполнении:
using (ApplicationContext db = new ApplicationContext())
{
    var user = db.Users.FirstOrDefault();
    if (user != null)
    {
        user.Age = 18;
        db.SaveChanges();
    }

    var users = db.Users.ToList();
    foreach (var u in users)
        Console.WriteLine($"{u.Name} ({u.Age})");
}

// Мы видим, что в наборе users первый элемент имеет у свойства Age значение 18.

// Причем в данном случае мы можем и не вызывать метод SaveChanges, элемент уже
// и так будет закэширован. Метод SaveChanges необходим, чтобы применить все изменения
// с объектами в базе данных.

// Но если бы мы использовали метод AsNoTracking(), то результат был бы другой:

using (ApplicationContext db = new ApplicationContext())
{
    var user = db.Users.AsNoTracking().FirstOrDefault();
    if (user != null)
    {
        user.Age = 22;
        db.SaveChanges();
    }

    var users = db.Users.AsNoTracking().ToList();
    foreach (var u in users)
        Console.WriteLine($"{u.Name} ({u.Age})");
}

// Свойство ChangeTracker
// Кроме использования метода AsNoTracking, можно отключить отслеживание в целом для объекта контекста.
// Для этого надо установить значение QueryTrackingBehavior.NoTracking для свойства db.ChangeTracker.
// QueryTrackingBehavior:

using (ApplicationContext db = new ApplicationContext())
{
    db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    var user = db.Users.FirstOrDefault();
    if (user != null)
    {
        user.Age = 8;
        db.SaveChanges();
    }

    var users = db.Users.ToList();
    foreach (var u in users)
        Console.WriteLine($"{u.Name} ({u.Age})");
}

// Вобще через свойство ChangeTracker мы можем управлять отслеживанием объектом и получать разнообразную информацию.
// Например, мы можем узнать, сколько объектов отслеживается в текущий момент:

using (ApplicationContext db = new ApplicationContext())
{
    var users = db.Users.ToList();

    foreach (var u in users)
        Console.WriteLine($"{u.Name} ({u.Age})");

    int count = db.ChangeTracker.Entries().Count();
    Console.WriteLine($"{count}");
}

// Отслеживаемые запросы:
using (ApplicationContext db = new ApplicationContext())
{
    var user1 = db.Users.FirstOrDefault();
    var user2 = db.Users.FirstOrDefault();
    if (user1 != null && user2 != null)
    {
        Console.WriteLine($"Before User1: {user1.Name}   User2: {user2.Name}");

        user1.Name = "Bob";

        Console.WriteLine($"After User1: {user1.Name}   User2: {user2.Name}");
    }
}

// Консольный вывод:
// Before User1: Tom   User2: Tom
// After User1: Bob   User2: Bob

// Так как запрос db.Users.FirstOrDefault() является отслеживаемым, то при получении данных, по ним будет создаваться
// объект user1, который добавляется в контекст и начинает отслеживаться.

// Далее повторно вызывается данный запрос для получения объекта user2. Этот запрос то же является отслеживаемым,
// поэтому EF увидит, что такой объект уже есть в контексте, он уже отслеживается, и возвратит ссылку на тот же объект. Поэтому все изменения с переменной user1 затронут и переменную user2.

// Рассмотрим другой пример:
using (ApplicationContext db = new ApplicationContext())
{
    var user1 = db.Users.FirstOrDefault();
    var user2 = db.Users.AsNoTracking().FirstOrDefault();

    if (user1 != null && user2 != null)
    {
        Console.WriteLine($"Before User1: {user1.Name}   User2: {user2.Name}");

        user1.Name = "Bob";

        Console.WriteLine($"After User1: {user1.Name}   User2: {user2.Name}");
    }
    // Консольный вывод:

    // Before User1: Tom   User2: Tom
    // After User1: Bob   User2: Tom
    // С первым объектом user1 все по прежнему: он также попадает в контекст и отслеживается. А вот второй запрос теперь
    // является неотслеживаемым, так как использует метод AsNoTracking. Поэтому для данных, полученных в результате этого
    // запроса, будет создаваться новый объект, который никак не будет связан с объектом user1. И изменения одного из
    // этих объектов никак не повлияют на второй объект.
}

// При вызове методов LINQ мы только создаем запрос. Его непосредственное выполнение происходит, когда мы начинаем
// потреблять результаты этого запроса. Нередко это происходит при переборе результата запроса в цикле for или при
// применении к нему ряда методов - ToList или ToArray, а также если запрос представляет скалярное значение, например,
// метод Count.

// В процессе выполнения запросов LINQ to Entities мы может получать два объекта, которые предоставляют наборы данных:
// IEnumerable и IQueryable. С одной стороны, интерфейс IQueryable наследуется от IEnumerable, поэтому по идее объект
// IQueryable это и есть также объект IEnumerable. Но реальность несколько сложнее. Между объектами этих интерфейсов
// есть разница в плане функциональности, поэтому они не взаимозаменяемы.

// Интерфейс IEnumerable находится в пространстве имен System.Collections и System.Collections.Generic (обобщенная
// версия). Объект IEnumerable представляет набор данных в памяти и может перемещаться по этим данным только вперед.
// Запрос, представленный объектом IEnumerable, выполняется немедленно и полностью, поэтому получение данных
// приложением происходит быстро.

// При выполнении запроса IEnumerable загружает все данные, и если нам надо выполнить их фильтрацию, то сама
// фильтрация происходит на стороне клиента.

// Интерфейс IQueryable располагается в пространстве имен System.Linq. Объект IQueryable предоставляет удаленный
// доступ к базе данных и позволяет перемещаться по данным как в прямом порядке от начала до конца, так и в обратном
// порядке. В процессе создания запроса, возвращаемым объектом которого является IQueryable, происходит оптимизация
// запроса. В итоге в процессе его выполнения тратится меньше памяти, меньше пропускной способности сети, но в то же
// время он может обрабатываться чуть медленнее, чем запрос, возвращающий объект IEnumerable.

// Возьмем два вроде бы идентичных выражения. Объект IEnumerable:
using (ApplicationContext db = new ApplicationContext())
{
    IEnumerable<User> userIEnum = db.Users;
    var users = userIEnum.Where(p => p.Id < 10).ToList();
    // Для примера здесь получаем всех пользователей, у которых id меньше 10.
    // Здесь запрос будет иметь следующий вид:
    // SELECT "u"."Id", "u"."Name"
    // FROM "Users" AS "u"
    // Фильтрация результата, обозначенная с помощью метода Where(p => p.Id > id) будет идти уже после выборки из бд
    // в самом приложении.
    // Чтобы совместить фильтры, нам надо было сразу применить метод Where: db.Users.Where(p => p.Id < 10);

    foreach (var user in users) Console.WriteLine(user.Name);
}

// Объект IQueryable:
using (ApplicationContext db = new ApplicationContext())
{
    IQueryable<User> userIQuer = db.Users;
    var users = userIQuer.Where(p => p.Id < 10).ToList();
    // Здесь запрос будет иметь следующий вид:
    // SELECT "u"."Id", "u"."Name"
    // FROM "Users" AS "u"
    // WHERE "u"."Id" < 10
    foreach (var user in users) Console.WriteLine(user.Name);
}

// Таким образом, все методы суммируются, запрос оптимизируется, и только потом, когда идет обращеие к методу ToList(),
// происходит выборка из базы данных.

// Это позволяет динамически создавать сложные запросы. Например, мы можем последовательно наслаивать в зависимости от
// условий выражения для фильтрации:
using (ApplicationContext db = new ApplicationContext())
{
    IQueryable<User> userIQuer = db.Users;
    userIQuer = userIQuer.Where(p => p.Id < 10);
    userIQuer = userIQuer.Where(p => p.Name == "Tom");
    var users = userIQuer.ToList();
    // В данном случае будет создаваться следующий SQL-запрос:
    // SELECT "u"."Id", "u"."Name"
    // FROM "Users" AS "u"
    // WHERE("u"."Id" < 10) AND("u"."Name" = 'Tom')

    foreach (var user in users) Console.WriteLine(user.Name);
}