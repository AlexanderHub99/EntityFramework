using LINQtoEntities;
using LINQtoEntities.Model;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");
// Добавляем данные в базу данных
using (ApplicationContext db = new ApplicationContext())
{
    Console.WriteLine("Добавляем данные в базу данных");

    await db.Database.EnsureDeletedAsync();
    await db.Database.EnsureCreatedAsync();

    Company google = new Company{ Name = "Google"};
    Company microsoft = new Company{ Name = "Microsoft"};
    await db.Companies.AddRangeAsync(google, microsoft);

    User tom = new User{ Name = "tom" , Age = 23 , Company = google };
    User kate = new User{ Name = "kate " , Age = 33 , Company = google };
    User bob = new User{ Name = "bob " , Age = 34 , Company = microsoft };
    User alice = new User{ Name = "alice " , Age = 54 , Company = microsoft };

    await db.Users.AddRangeAsync(tom, kate, bob, alice);
    await db.SaveChangesAsync();
    Scripts.SplitСonsole();
}

using (ApplicationContext db = new ApplicationContext())
{
    var users = await (from user in db.Users.Include(p => p.Company)  //  Указываем связанную сущность для запроса 
                       where user.CompanyId == 1          // Выполняет фильтрацию последовательности значений на основе заданного предиката.
                       select user).ToListAsync();        // Асинхронное получение данных

    Console.WriteLine("Вывод всех объектов с CompanyId == 1");
    foreach (var user in users)
    {
        Console.WriteLine($"{user.Name}({user.Age}) - {user.Company?.Name}");
    }

    Scripts.SplitСonsole();
}

using (ApplicationContext db = new ApplicationContext())
{
    var users = await db.Users
                        .Include(p => p.Company)          // Указываем связанную сущность для запроса 
                        .Where(p => p.CompanyId == 2)     // Выполняет фильтрацию последовательности значений на основе заданного предиката.
                        .ToListAsync();                   // Асинхронное получение данных

    Console.WriteLine("Вывод всех объектов с CompanyId == 2");
    foreach (var user in users)
    {
        Console.WriteLine($"{user.Name}({user.Age}) - {user.Company?.Name}");
    }

    Scripts.SplitСonsole();
}

using (ApplicationContext contextDb = new ApplicationContext())
{
    var usersGoogle = contextDb.Users.Where(p => p.Company!.Name == "Google");

    // Аналогичный запос с помощью операторов LINQ:
    var usersMicrosoft = await (from user in contextDb.Users        // Указываем связанную сущность для запроса
                                where user.Company!.Name == "Microsoft"   // Выполняет фильтрацию последовательности значений на основе заданного предиката.
                                select user).ToListAsync();         // Асинхронное получение данных

    Console.WriteLine($"Company(Google)Users");

    foreach (var user in usersGoogle)
    {
        Console.WriteLine($"{user.Name}({user.Age})");
    }

    Console.WriteLine("\n");
    Console.WriteLine($"Company(Microsoft)Users");

    foreach (var user in usersMicrosoft)
    {
        Console.WriteLine($"{user.Name}({user.Age})");
    }

    Scripts.SplitСonsole();
}

using (ApplicationContext db = new ApplicationContext())
{
    // Метод принимает два параметра - оцениваемое выражение и шаблон, с которым сравнивается его значение. Например,
    // найдем всех пользователей, в имени которых присутствует подстрока "Tom" (это могут быть "Tom", "Tomas", "Tomek",
    // "Smith Tom"):
    var users = db.Users.Where(p => EF.Functions.Like(p.Name!, "%Tom%"));

    Console.WriteLine("Вывод всех объектов с с совпадением  Tom");
    foreach (User user in users)
    {
        Console.WriteLine($"{user.Name} ({user.Age})");
    }

    Scripts.SplitСonsole();
}

// Find/FindAsync
using (ApplicationContext db = new ApplicationContext())
{
    User? user = db.Users.Find(3); // выберем элемент с id=3
    // асинхронная версия
    // User? user = await db.Users.FindAsync(3); // выберем элемент с id=3

    Console.WriteLine("Вывод всех объектов c id == 3");
    if (user != null) Console.WriteLine($"{user.Name} ({user.Age})");

    Scripts.SplitСonsole();
}

using (ApplicationContext db = new ApplicationContext())
{
    // Найдем всех пользователей у которых возраст (свойство Age) в диапазоне от 20 до 29
    var usersAge20_29 = db.Users.Where(u => EF.Functions.Like(u.Age.ToString(), "2%"));

    // Подобным образом метод EF.Functions.Like() можно использовать с операторами LINQ:
    var users2 = from u in db.Users
                 where EF.Functions.Like(u.Age.ToString(), "2%")
                 select u;

    Console.WriteLine("Вывод всех объектов  у которых возраст (свойство Age) в диапазоне от 20 до 29");
    foreach (User user in usersAge20_29)
    {
        Console.WriteLine($"{user.Name} ({user.Age})");
    }

    Scripts.SplitСonsole();
}

// First/FirstOrDefault/FirstAsync/FirstOrDefaultAsync
// Но в качестве альтернативы мы можем использовать методы Linq First()/FirstOrDefault() и их асинхронные версии
// FirstAsync()/FirstOrDefaultAsync(). Они получают первый элемент выборки, который соответствует определенному
// условию или набору условий. Использование метода FirstOrDefault() является более гибким, так как если выборка
// пуста, то он вернет значение null. А метод First() в той же ситуации выбросит ошибку.

using (ApplicationContext db = new ApplicationContext())
{
    User? user1 = db.Users.FirstOrDefault();

    Console.WriteLine("Вывод первого объекта из таблице");
    // асинхронная версия
    // User? user = await db.Users.FirstOrDefaultAsync();
    if (user1 != null)
    {
        Console.WriteLine(user1.Name);
    }

    User? user2 = db.Users.FirstOrDefault(p=>p.Id==3);

    Console.WriteLine("Вывод объекта с Id==3");
    // асинхронная версия
    // User? user = await db.Users.FirstOrDefaultAsync(p=>p.Id==3);
    if (user2 != null)
    {
        Console.WriteLine(user2.Name);
    }
    Scripts.SplitСonsole();
}


// Допустим, нам надо добавить в результат выборки название компании. Мы можем использовать метод Include для подсоединения
// к объекту связанных данных из другой таблицы: var users = db.Users.Include(p=>p.Company). Но не всегда нужны все свойства
// выбираемых объектов. В этом случае мы можем применить метод Select для проекции извлеченных данных на новый тип:
using (ApplicationContext db = new ApplicationContext())
{
    var users1 = db.Users.Select(p => new
    {
        Name = p.Name,
        Age = p.Age,
        Company = p.Company!.Name
    });

    Console.WriteLine("Вывод нового объекта с помощью метода Select");
    foreach (var user in users1)
    {
        Console.WriteLine($"{user.Name} ({user.Age}) - {user.Company}");
    }

    Console.WriteLine("Вывод нового объекта с помощью метода Select");
    // В даном случае мы получим данные анонимного типа, но это также может быть определенный пользователем тип.Например:
    var users2 = db.Users.Select(p => new UserModel
    {
        Name = p.Name,
        Age = p.Age,
        Company = p.Company!.Name
    });
    foreach (UserModel user in users2)
    {
        Console.WriteLine($"{user.Name} ({user.Age}) - {user.Company}");
    }

    Console.WriteLine("Вывод по убыванию применяется метод OrderByDescending()");
    // Для сортировки по убыванию применяется метод OrderByDescending():
    var users3 = db.Users.OrderByDescending(u=>u.Name);
    foreach (UserModel user in users2)
    {
        Console.WriteLine($"{user.Name} ({user.Age}) - {user.Company}");
    }

    // При необходимости упорядочить данные сразу по нескольким критериям можно использовать методы ThenBy()(для сортировки
    // по возрастанию) и ThenByDescending(). Например, отсортируем по двум значениям:
    var users4 = db.Users.OrderBy(u => u.Age).ThenBy(u=>u.Company!.Name);
    foreach (var user in users4)
    {
        Console.WriteLine($"{user.Name} ({user.Age}) - {user.Company}");
    }
    Scripts.SplitСonsole();
}

// Соединение и группировка таблиц
using (ApplicationContext db = new ApplicationContext())
{
    var users = db.Users.Join(db.Companies, // второй набор
        u => u.CompanyId, // свойство-селектор объекта из первого набора
        c => c.Id, // свойство-селектор объекта из второго набора
        (u, c) => new // результат
        {
            Name=u.Name,
            Company = c.Name,
            Age=u.Age
        });
    // Метод Join принимает четыре параметра:
    // вторую таблицу, которая соединяется с текущей
    // свойство объекта - столбец из первой таблицы, по которому идет соединение
    // свойство объекта -столбец из второй таблицы, по которому идет соединение
    // новый объект, который получается в результате соединения
    // В итоге данный запрос будет транслироваться в следующее выражение SQL:
    // SELECT "u"."Name", "c"."Name" AS "Company", "u"."Age"
    // FROM "Users" AS "u"
    // INNER JOIN "Companies" AS "c" ON "u"."CompanyId" = "c"."Id"

    Console.WriteLine("Вывод cоединенных и группированных таблиц");
    foreach (var u in users)
    {
        Console.WriteLine($"{u.Name} ({u.Company}) - {u.Age}");
    }
    Scripts.SplitСonsole();
}

// Объединим три таблицы в один набор:
using (ApplicationContext db = new ApplicationContext())
{
    // пересоздаем базу данных
    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();

    Country usa = new Country { Name = "USA" };

    Company microsoft = new Company { Name = "Microsoft", Country = usa };
    Company google = new Company { Name = "Google", Country = usa };
    db.Companies.AddRange(microsoft, google);

    User tom = new User { Name = "Tom", Age = 36, Company = microsoft };
    User bob = new User { Name = "Bob", Age = 39, Company = google };
    User alice = new User { Name = "Alice", Age = 28, Company = microsoft };
    User kate = new User { Name = "Kate", Age = 25, Company = google };

    db.Users.AddRange(tom, bob, alice, kate);
    db.SaveChanges();
}

using (ApplicationContext db = new ApplicationContext())
{
    var users = from user in db.Users
                join company in db.Companies on user.CompanyId equals company.Id
                join country in db.Countries on company.CountryId equals country.Id
                select new
                {
                    Name = user.Name,
                    Company = company.Name,
                    Age = user.Age,
                    Country = country.Name
                };

    Console.WriteLine("Вывод объединенных три таблицы в один набор:");
    foreach (var u in users)
    {
        Console.WriteLine($"{u.Name} ({u.Company} - {u.Country}) - {u.Age}");
    }
    Scripts.SplitСonsole();
}

// Группировка
using (ApplicationContext db = new ApplicationContext())
{
    var  groups = from u in db.Users
                  group u by u.Company!.Name into g
                  select new
                  {
                      g.Key,
                      Count = g.Count()
                  };
    foreach (var group in groups)
    {
        Console.WriteLine($"{group.Key} - {group.Count}");
    }


    // Аналогично работает метод GroupBy():
    var groups2 = db.Users.GroupBy(u => u.Company!.Name).Select(g => new
    {
        g.Key,
        Count = g.Count()
    });

    Console.WriteLine("Вывод группировки:");
    foreach (var group in groups2)
    {
        Console.WriteLine($"{group.Key} - {group.Count}");
    }
    Scripts.SplitСonsole();
}

// Объединение
using (ApplicationContext db = new ApplicationContext())
{
    var users = db.Users.Where(u => u.Age < 30)
        .Union(db.Users.Where(u=>u.Name!.Contains("Tom")));

    Console.WriteLine("Вывод объединение:");
    foreach (var user in users)
    {
        Console.WriteLine(user.Name);
    }
    Scripts.SplitСonsole();
}

// Пересечение
using (ApplicationContext db = new ApplicationContext())
{
    // Чтобы найти пересечение выборок, то есть те элементы, которые присутствуют сразу в двух выборках,
    // используется метод Intersect(). Например, выберем все пользователей, у которых возраст больше 30
    // и в имени содержится подстрока "Tom":
    var users = db.Users.Where(u => u.Age > 30)
        .Intersect(db.Users.Where(u=>u.Name!.Contains("Tom")));

    Console.WriteLine("Вывод Пересечение:");
    foreach (var user in users)
    {
        Console.WriteLine(user.Name);
    }
    Scripts.SplitСonsole();
}

// Разность
using (ApplicationContext db = new ApplicationContext())
{
    var selector1 = db.Users.Where(u => u.Age > 30); // 
    var selector2 = db.Users.Where(u => u.Name!.Contains("Tom"));
    // Если нам надо найти элементы первой выборки, которые отсутствуют во второй выборке, то мы можем
    // использовать метод Except:
    var users = selector1.Except(selector2);

    foreach (var user in users)
    {
        Console.WriteLine(user.Name);
    }
    Scripts.SplitСonsole();
}


using (ApplicationContext db = new ApplicationContext())
{
    // Наличие элементов

    // Метод Any() позволяет проверить, есть ли в базе данных элемент с определенными признаками, и если есть,
    // то метод возвратит значение true. Например, проверим, есть ли в базе данных пользователи, которые работают
    // в компании Google:
    bool result1 = db.Users.Any(u=>u.Company!.Name=="Google");

    Console.WriteLine(" Наличие элементов User.Company!.Name==Google");
    Console.WriteLine(result1.ToString());

    // Метод All() позволяет проверит, удовлетворяют ли все элементы в базе данных определенному критерию. Например,
    // проверим, все ли пользователи работают в компании Microsoft
    bool result2 = db.Users.All(u=>u.Company!.Name=="Microsoft");

    Console.WriteLine("Удовлетворяют ли все элементы в базе данных определенному критерию User.Company!.Name==Google");
    Console.WriteLine(result2.ToString());

    // Количество элементов в выборке
    // Метод Count() позволяет найти количество элементов в выборке:
    int number1 = db.Users.Count();
    // найдем кол-во пользователей, которые в имени содержат подстроку Tom
    int number2 = db.Users.Count(u => u.Name!.Contains("Tom"));

    Console.WriteLine("Количество элементов в выборке Users");
    Console.WriteLine(number1);
    Console.WriteLine("Количество элементов в выборке User.Name!.Contains(Tom)");
    Console.WriteLine(number2);

    // Минимальное, максимальное и среднее значения
    // Для нахождения минимального, максимального и среднего значений по выборке применяются функции Min(), Max() и
    // Average() соответственно. Найдем минимальный, максимальный и средний возраст пользователей:

    // минимальный возраст
    int minAge = db.Users.Min(u=>u.Age);
    // максимальный возраст
    int maxAge = db.Users.Max(u=>u.Age);
    // средний возраст пользователей, которые работают в Microsoft
    double avgAge = db.Users.Where(u=>u.Company!.Name=="Microsoft")
                        .Average(p => p.Age);

    Console.WriteLine("Минимальный возраст");
    Console.WriteLine(minAge);
    Console.WriteLine("Максимальный возраст");
    Console.WriteLine(maxAge);
    Console.WriteLine("Cредний возраст пользователей, которые работают в Microsoft");
    Console.WriteLine(avgAge);

    // Сумма значений
    // Для получения суммы значений используется метод Sum():
    // суммарный возраст всех пользователей 
    int sum1 = db.Users.Sum(u => u.Age);
    // суммарный возраст тех, кто работает в Microsoft
    int sum2 = db.Users.Where(u=>u.Company!.Name == "Microsoft")
                        .Sum(u => u.Age);

    Console.WriteLine("Суммарный возраст всех пользователей");
    Console.WriteLine(sum1);
    Console.WriteLine("Суммарный возраст тех, кто работает в Microsoft");
    Console.WriteLine(sum2);
}
