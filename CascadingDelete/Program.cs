using CascadingDelete.Model;

Console.WriteLine("Hello, World!");

using (ApplicationContext db = new())
{
    // добавляем начальные данные
    Company microsoft = new Company { Name = "Microsoft" };
    Company google = new Company { Name = "Google" };
    db.Companies.AddRange(microsoft, google);
    db.SaveChanges();
    User tom = new User { Name = "Tom", Company = microsoft };
    User bob = new User { Name = "Bob", Company = google };
    User alice = new User { Name = "Alice", Company = microsoft };
    User kate = new User { Name = "Kate", Company = google };
    db.Users.AddRange(tom, bob, alice, kate);
    db.SaveChanges();

    // получаем пользователей
    var users = db.Users.ToList();
    foreach (var user in users) Console.WriteLine(user.Name);

    // Удаляем первую компанию
    var comp = db.Companies.FirstOrDefault();
    if (comp != null) db.Companies.Remove(comp);
    db.SaveChanges();
    Console.WriteLine("\nСписок пользователей после удаления компании");
    // снова получаем пользователей
    users = db.Users.ToList();
    foreach (var user in users) Console.WriteLine(user.Name);
    // Консольный вывод программы:
    // Bob
    // Tom
    // Alice
    // Kate
    //
    // Список пользователей после удаления компании
    // Bob
    // Kate

    //Удаление главной сущности - компании привело к удалению двух зависимых сущностей - пользователей.
}