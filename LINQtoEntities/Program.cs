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
}