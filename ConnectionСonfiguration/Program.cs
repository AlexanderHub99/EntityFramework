using ConnectionСonfiguration.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

Console.WriteLine("Hello, World!");

using (User1Context db = new User1Context())
{
    User user1 = new User { Name = "Sasha1", Age = 23 };
    User user2 = new User { Name = "Sasha2", Age = 34 };
    User user3 = new User { Name = "Sasha3", Age = 75 };

    await db.AddRangeAsync(user1, user2, user3);
    await db.SaveChangesAsync();


    Console.WriteLine("\nПервый вариант подключения к DB");

    var users = await db.Users.ToListAsync();
    foreach (var item in users)
    {
        Console.WriteLine($"{item.Id}.{item.Name}.{item.Age}");
    }
}

using (User2Context db = new User2Context("Data Source=TestUser2.db"))
{
    Console.WriteLine("\nВторой вариант подключения к DB");

    var users = await db.Users.ToListAsync();
    foreach (var item in users)
    {
        Console.WriteLine($"{item.Id}.{item.Name}");
    }
}

///Здесь опять же применяется метод UseSqlServer класса DbContextOptionsBuilder для создания конфигурации по той же строке подключения.
///Только результат этой операции - объект DbContextOptions затем передается в контекст данных. А контекст данных далее передает этот
///параметр в конструктор базового класса.
var optionsBuilder = new DbContextOptionsBuilder<User3Context>();
var options = optionsBuilder.UseSqlite("Data Source=TestUser2.db").Options;
using (User3Context db = new User3Context(options))
{
    Console.WriteLine("\nТретий вариант подключения к DB");

    var users = await db.Users.ToListAsync();
    foreach (User user in users)
    {
        Console.WriteLine($"{user.Id}.{user.Name} - {user.Age}");
    }
}


var builder = new ConfigurationBuilder();
// установка пути к текущему каталогу
builder.SetBasePath(Directory.GetCurrentDirectory());
// получаем конфигурацию из файла appsettings.json
builder.AddJsonFile("appsettings.json");
// создаем конфигурацию
var config = builder.Build();
// получаем строку подключения
string connectionString = config.GetConnectionString("DefaultConnection");

var optionsBuilder1 = new DbContextOptionsBuilder<User4Context>();
var options1 = optionsBuilder1.UseSqlite(connectionString).Options;
using (User4Context db = new User4Context(options1))
{
    Console.WriteLine("\nЧетвертый вариант подключения к DB");

    var users =await db.Users.ToListAsync();
    foreach (User user in users)
    {
        Console.WriteLine($"{user.Id}.{user.Name} - {user.Age}");
    }
}