using LINQtoEntities;
using LINQtoEntities.Model;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");
// Добавляем данные в базу данных
using (ApplicationContext db = new ApplicationContext())
{
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
}

using (ApplicationContext db = new ApplicationContext())
{
    var users = await (from user in db.Users.Include(p => p.Company)  //  Указываем связанную сущность для запроса 
                       where user.CompanyId == 1          // Выполняет фильтрацию последовательности значений на основе заданного предиката.
                       select user).ToListAsync();        // Асинхронное получение данных

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
    // асинхронная версия
    // User? user = await db.Users.FirstOrDefaultAsync();
    if (user1 != null)
    {
        Console.WriteLine(user1.Name);
    }

    User? user2 = db.Users.FirstOrDefault(p=>p.Id==3);
    // асинхронная версия
    // User? user = await db.Users.FirstOrDefaultAsync(p=>p.Id==3);
    if (user2 != null)
    {
        Console.WriteLine(user2.Name);
    }
    Scripts.SplitСonsole();
}