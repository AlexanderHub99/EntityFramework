using LazyLoading.Model;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");

using (ApplicationContext db = new ApplicationContext())
{
    // пересоздадим базу данных
    await db.Database.EnsureDeletedAsync();
    await db.Database.EnsureCreatedAsync();

    // добавляем начальные данные
    Company google = new Company { Name = "Google"};
    Company microsoft = new Company { Name = "Microsoft" };
    await db.Companies.AddRangeAsync(microsoft, google);


    User bob = new User { Name = "Bob", Company = google };
    User kate = new User { Name = "Kate", Company = google };
    User tom = new User { Name = "Tom", Company = microsoft };
    User alice = new User { Name = "Alice", Company = microsoft };
    await db.Users.AddRangeAsync(tom, bob, alice, kate);

    await db.SaveChangesAsync();
}
using (ApplicationContext db = new ApplicationContext())
{
    // Теперь посмотрим, что происходит на уровне базы данныx. Вначале получаем пользователей:
    // В базе данных выполняется sql-команда:
    // SELECT "u"."Id", "u"."CompanyId", "u"."Name"
    // FROM "Users" AS "u"
    var users = await db.Users.ToListAsync();

    // Далее в цикле перебираем всех полученных пользователей и обращаемся к навигационному
    // свойству Company для получения связанной компании:
    // Поскольку идет обращение к навигационному свойству Company, то автоматически подтягиваются
    // связанные с ним данные - объекты Company. В данном случае у нас выше было добавлено только
    // 2 компании и обе эти компании ссвязанные с перебираемыми пользователями: два пользователя
    // связаны с одной компанией, а два других пользователя - с другой. Поэтому будут выполняться
    // два запроса:
    foreach (User user in users)
        Console.WriteLine($"{user.Name} - {user.Company?.Name}");
    // То есть если совместить консольный вывод и выполняемые выражения SQL, то получится следующим образом:
    // получение всех пользователей
    /////// SELECT "u"."Id", "u"."CompanyId", "u"."Name"
    /////// FROM "Users" AS "u"
    // идет обращение к свойству Company, его компании нет в контексте
    // поэтому выполняется sql-запрос
    /////// SELECT "c"."Id", "c"."Name"
    /////// FROM "Companies" AS "c"
    /////// WHERE "c"."Id" = @__p_0
    // Tom - Microsoft
    // компания этого пользователя уже в контексте, не надо выполнять новый запрос
    // Alice - Microsoft
    // компании следующего пользователя нет в контексте
    // поэтому выполняется sql-запрос
    /////// SELECT "c"."Id", "c"."Name"
    /////// FROM "Companies" AS "c"
    /////// WHERE "c"."Id" = @__p_0
    // Bob - Google
    // компания этого пользователя уже в контексте, не надо выполнять новый запрос
    // Kate - Google
}

// Таким же образом можно загрузить компании и связанных с ними пользователей:
using (ApplicationContext db = new ApplicationContext())
{
    var companies = await db.Companies.ToListAsync();
    foreach (Company company in companies)
    {
        Console.Write($"{company.Name}:");

        foreach (User user in company.Users)
        {
            Console.Write($"{user.Name} ");
            Console.WriteLine();
        }
    }

    //Однако при использовании lazy loading следует учитывать что если в базе данных произошли какие-нибудь изменения,
    //например, другой пользователь изменил данные, то данные не перезагужаются, контекст продолжает использовать те
    //данные, которые были ранее загружены, как собственно было показано выше.
}