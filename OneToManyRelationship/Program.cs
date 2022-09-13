using Microsoft.EntityFrameworkCore;
using OneToManyRelationship.Model;

Console.WriteLine("Hello, World!");

// Adding data:
using (ApplicationContext db = new ApplicationContext())
{
    // пересоздадим базу данных
    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();

    // создание и добавление моделей
    Company microsoft = new Company { Name = "Microsoft" };
    Company google = new Company { Name = "Google" };
    db.Companies.AddRange(microsoft, google);

    User tom = new User { Name = "Tom", Company = microsoft };
    User bob = new User { Name = "Bob", Company = microsoft };
    User alice = new User { Name = "Alice", Company = google };
    db.Users.AddRange(tom, bob, alice);
    db.SaveChanges();
}

// Getting data:
using (ApplicationContext db = new ApplicationContext())
{
    // вывод пользователей
    var users = db.Users.Include(u => u.Company).ToList();
    foreach (User user in users)
        Console.WriteLine($"{user.Name} - {user.Company?.Name}");

    // вывод компаний
    var companies = db.Companies.Include(c => c.Users).ToList();
    foreach (Company comp in companies)
    {
        Console.WriteLine($"\n Компания: {comp.Name}");
        foreach (User user in comp.Users)
        {
            Console.WriteLine($"{user.Name}");
        }
    }
}

// Editing:
using (ApplicationContext db = new ApplicationContext())
{
    // изменение имени пользователя
    User? user1 = db.Users.FirstOrDefault(p => p.Name == "Tom");
    if (user1 != null)
    {
        user1.Name = "Tomek";
        db.SaveChanges();
    }
    // изменение названия компании
    Company? comp = db.Companies.FirstOrDefault(p => p.Name == "Google");
    if (comp != null)
    {
        comp.Name = "Alphabet";
        db.SaveChanges();
    }

    // смена компании сотрудника
    User? user2 = db.Users.FirstOrDefault(p => p.Name == "Bob");
    if (user2 != null && comp != null)
    {
        user2.Company = comp;
        db.SaveChanges();
    }
}

// Removal:
using (ApplicationContext db = new ApplicationContext())
{
    User? user = db.Users.FirstOrDefault(u => u.Name == "Bob");
    if (user != null)
    {
        db.Users.Remove(user);
        db.SaveChanges();
    }

    Company? comp = db.Companies.FirstOrDefault();
    if (comp != null)
    {
        db.Companies.Remove(comp);
        db.SaveChanges();
    }
}