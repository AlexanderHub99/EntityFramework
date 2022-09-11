using ExplicitLoading.Model;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");

using (ApplicationContext db = new ApplicationContext())
{
    // пересоздадим базу данных
    await db.Database.EnsureDeletedAsync();
    await db.Database.EnsureCreatedAsync();

    // добавляем начальные данные
    Company microsoft = new Company { Name = "Microsoft" };
    Company google = new Company { Name = "Google" };
    await db.Companies.AddRangeAsync(microsoft, google);


    User tom = new User { Name = "Tom", Company = microsoft };
    User bob = new User { Name = "Bob", Company = google };
    User alice = new User { Name = "Alice", Company = microsoft };
    User kate = new User { Name = "Kate", Company = google };
    await db.Users.AddRangeAsync(tom, bob, alice, kate);

    await db.SaveChangesAsync();
}
using (ApplicationContext db = new ApplicationContext())
{
    Company? company = db.Companies.FirstOrDefault();
    if (company != null)
    {
        // Выражение db.Users.Where(p=>p.CompanyId==company.Id).Load() загружает пользователей в контекст.
        // Подвыражение Where(p=>p.CompanyId==company.Id) означает, что загружаются только те пользователи,
        // у которых свойство CompanyId соответствует свойству Id ранее полученной компании. После этого нам
        // не надо подгружать связанные данные, так как они уже есть в контексте.
        await db.Users.Where(u => u.CompanyId == company.Id).LoadAsync();

        Console.WriteLine($"Company: {company.Name}");
        foreach (var u in company.Users)
            Console.WriteLine($"User: {u.Name}");

        //Важно, что здесь подгружаются только те пользователи, которые непосредственно связаны с компанией.
        //Если нам надо загрузить в контекст вообще все объекты из таблицы Users, то можно было бы использовать
        //следующее выражение db.Users.Load()
    }
}

using (ApplicationContext db = new ApplicationContext())
{
    Company? company = db.Companies.FirstOrDefault();
    if (company != null)
    {
        // Для загрузки связанных данных мы также можем использовать методы Collection() и Reference. Метод Collection
        // применяется, если навигационное свойство представляет коллекцию:
        db.Entry(company).Collection(c => c.Users).Load();   // Получаем коллекцию сотрудников которые работают в компании

        Console.WriteLine($"Company: {company.Name}");
        foreach (var u in company.Users)
            Console.WriteLine($"User: {u.Name}");
    }
}

using (ApplicationContext db = new ApplicationContext())
{
    User? user = db.Users.FirstOrDefault();  // получаем первого пользователя
    if (user != null)
    {
        db.Entry(user).Reference(u => u.Company).Load();   // Получаем компанию в которой работает пользователь
        Console.WriteLine($"{user.Name} - {user.Company?.Name}");   // Tom - Microsoft
    }
}