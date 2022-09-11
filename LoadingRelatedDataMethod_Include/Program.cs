using LoadingRelatedDataMethod_Include.Model;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");


using (ApplicationContext db = new ApplicationContext())
{
    // Пересоздание базы данных
    await db.Database.EnsureDeletedAsync();
    await db.Database.EnsureCreatedAsync();

    // Добовляем начальные данные 
    Company google = new Company{ Name = "Google"};
    Company mictosoft = new Company{ Name = "Mictosoft"};
    await db.Companies.AddRangeAsync(mictosoft, google);

    User bob = new User{ Name = "Bob" , Age = 33 , Company = google};
    User kate = new User{ Name = "Kate" , Age = 45 , Company = google};
    User tom = new User{ Name = "Tom" , Age = 23 , Company = mictosoft};
    User alica = new User{ Name = "Alica" , Age = 26 , Company = mictosoft};
    await db.Users.AddRangeAsync(tom, alica, bob, kate);

    await db.SaveChangesAsync();

    // Получаем пользователей 
    // Для загрузки связанных данных используется метод Include:
    var users = await db.Users
       // Использование бессмысленно так как в контекст уже загружены данные
       // .Include(u => u.Company)  // подгружаем данные по компаниям
        .ToListAsync();

    foreach (var user in users)
    {
        Console.WriteLine($"{user.Name} - {user.Company?.Name}");
    }
    Console.WriteLine("----------------------");      // для красоты
}

using (ApplicationContext ctx = new ApplicationContext())
{
    var companies = await ctx.Companies.ToListAsync();

    var users = await ctx.Users
       // Использование бессмысленно так как в контекст уже загружены данные
        .Include(u => u.Company)  // подгружаем данные по компаниям
        .ToListAsync();

    foreach (var user in users)
    {
        Console.WriteLine($"{user.Name} - {user.Company?.Name}");
    }
    Console.WriteLine("----------------------");        // для красоты
}
// Здесь программа логически разделена на две части: добавление объектов и их получение. Для каждой части создается
// свой объект ApplicationContext. В итоге при получении объект ApplicationContext не будет ничего знать об объектах,
// которые были добавлены в области действия другого объекта ApplicationContext. Поэтому в этом случае, если мы хотим
// получить связанные данные, нам необходимо использовать метод Include.
using (ApplicationContext db = new ApplicationContext())
{
    var users = await db.Users
        .Include(u => u.Company)  // добавляем данные по компаниям
        .ToListAsync();

    foreach (var user in users)
        Console.WriteLine($"{user.Name} - {user.Company?.Name}");
    Console.WriteLine("----------------------");         // для красоты
}

using (ApplicationContext db = new ApplicationContext())
{
    var companies = await db.Companies
                    .Include(c => c.Users)  // добавляем данные по пользователям
                    .ToListAsync();
    foreach (var company in companies)
    {
        Console.WriteLine(company.Name);
        // выводим сотрудников компании
        foreach (var user in company.Users)
            Console.WriteLine(user.Name);
        Console.WriteLine("----------------------");     // для красоты
    }
    Console.WriteLine("----------------------");         // для красоты
}

// добавление данных
using (ApplicationContext db = new ApplicationContext())
{
    // пересоздадим базу данных
    await db.Database.EnsureDeletedAsync();
    await db.Database.EnsureCreatedAsync();

    Country usa = new Country { Name = "USA" };
    Country japan = new Country { Name = "Japan" };
    await db.Countries.AddRangeAsync(usa, japan);

    // добавляем начальные данные
    Company2 microsoft = new Company2 { Name = "Microsoft", Country = usa };
    Company2 sony = new Company2 { Name = "Sony", Country = japan };
    await db.Companies2.AddRangeAsync(microsoft, sony);


    User2 tom = new User2 { Name = "Tom", Company2 = microsoft };
    User2 bob = new User2 { Name = "Bob", Company2 = sony };
    User2 alice = new User2 { Name = "Alice", Company2 = microsoft };
    User2 kate = new User2 { Name = "Kate", Company2 = sony };
    await db.User2s.AddRangeAsync(tom, bob, alice, kate);

    await db.SaveChangesAsync();
}
// ThenInclude
// Допустим, вместе с пользователями мы хотим загрузить и страны, в которых базируются компании пользователей.
// То есть получается, что нам нужно спуститься еще на уровень ниже: User - Company - Country.Для этого нам надо
// применить метод ThenInclude(), который работает похожим образом, что и Include:
// получение данных
using (ApplicationContext db = new ApplicationContext())
{
    // получаем пользователей
    var users = await db.User2s
        .Include(u => u.Company2)  // подгружаем данные по компаниям
            .ThenInclude(c => c!.Country)    // к компаниям подгружаем данные по странам
        .ToListAsync();
    foreach (var user in users)
        Console.WriteLine($"{user.Name} - {user.Company2?.Name} - {user.Company2?.Country?.Name}");
}

// Также мы можем использовать тот же метод Include для загрузки данных далее по цепочке:
using (ApplicationContext db = new ApplicationContext())
{
    var users = await db.User2s
        .Include(u => u.Company2!.Country)
        .ToListAsync();
    foreach (var user in users)
        Console.WriteLine($"{user.Name} - {user.Company2?.Name} - {user.Company2?.Country!.Name}");
}

using (ApplicationContext db = new ApplicationContext())
{
    // пересоздадим базу данных
    await db.Database.EnsureDeletedAsync();
    await db.Database.EnsureCreatedAsync();

    Position manager = new Position { Name = "Manager" };
    Position developer = new Position { Name = "Developer" };
    await db.Positions.AddRangeAsync(manager, developer);

    City washington = new City { Name = "Washington" };
    await db.Cities.AddAsync(washington);

    Country2 usa = new Country2 { Name = "USA", Capital = washington };
    await db.Countries2.AddAsync(usa);

    Company3 microsoft = new Company3 { Name = "Microsoft", Country = usa };
    Company3 google = new Company3 { Name = "Google", Country = usa };
    await db.Companies3.AddRangeAsync(microsoft, google);

    User3 tom = new User3 { Name = "Tom", Company3 = microsoft, Position = manager };
    User3 bob = new User3 { Name = "Bob", Company3 = google, Position = developer };
    User3 alice = new User3 { Name = "Alice", Company3 = microsoft, Position = developer };
    User3 kate = new User3 { Name = "Kate", Company3 = google, Position = manager };
    await db.Users3.AddRangeAsync(tom, bob, alice, kate);

    await db.SaveChangesAsync();
}
using (ApplicationContext db = new ApplicationContext())
{
    // получаем пользователей
    var users = await db.Users3
                    .Include(u => u.Company3)  // добавляем данные по компаниям
                        .ThenInclude(comp => comp!.Country)      // к компании добавляем страну 
                            .ThenInclude(count => count!.Capital)    // к стране добавляем столицу
                    .Include(u => u.Position) // добавляем данные по должностям
                    .ToListAsync();
    foreach (var user in users)
    {
        Console.WriteLine($"{user.Name} - {user.Position?.Name}");
        Console.WriteLine($"{user.Company3?.Name} - {user.Company3?.Country?.Name} " +
                          $"- {user.Company3?.Country?.Capital?.Name}");
        Console.WriteLine("----------------------");     // для красоты
    }
}