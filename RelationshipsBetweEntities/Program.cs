using RelationshipsBetweEntities.Model;

Console.WriteLine("Hello, World!");

// Установка главной сущности по навигационному свойству зависимой сущности:
//Причем при использовании классов нам достаточно установить либо одно навигационное свойство, либо свойство-внешний
//ключ. Например, укажем значение только для навигационного свойства:
using (ApplicationContext db = new ApplicationContext())
{
    Company company1 = new Company { Name = "Google" };
    Company company2 = new Company { Name = "Microsoft" };
    User user1 = new User { Name = "Tom", Company = company1 };
    User user2 = new User { Name = "Bob", Company = company2 };
    User user3 = new User { Name = "Sam", Company = company2 };

    db.Companies.AddRange(company1, company2);  // добавление компаний
    db.Users.AddRange(user1, user2, user3);     // добавление пользователей
    db.SaveChanges();

    foreach (var user in db.Users.ToList())
    {
        Console.WriteLine($"{user.Name} работает в {user.Company?.Name}");
    }
}

// Установка главной сущности по свойству-внешнему ключу зависимой сущности:
// Здесь надо отметить один момент: для устновки свойства внешнего ключа CompanyId нам необходимо знать его значение.
// Однако посколько оно связано со свойством Id класса Company, значение которого генерируется при добавление объекта
// в БД, соответственно в данном случае необходимо сначала добавить объект Company в базу данных.
using (ApplicationContext db = new ApplicationContext())
{
    Company company1 = new Company { Name = "Google" };
    Company company2 = new Company { Name = "Microsoft" };
    db.Companies.AddRange(company1, company2);  // добавление компаний
    db.SaveChanges();

    User user1 = new User { Name = "Tom", CompanyId = company1.Id };
    User user2 = new User { Name = "Bob", CompanyId = company1.Id };
    User user3 = new User { Name = "Sam", CompanyId = company2.Id };

    db.Users.AddRange(user1, user2, user3);     // добавление пользователей
    db.SaveChanges();

    foreach (var user in db.Users.ToList())
    {
        Console.WriteLine($"{user.Name} работает в {user.Company?.Name}");
    }
}