using LoggingOperations.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

Console.WriteLine("Hello, World!");

var builder = new ConfigurationBuilder();
builder.SetBasePath(Directory.GetCurrentDirectory());// установка пути к текущему каталогу
builder.AddJsonFile("appsettings.json");// получаем конфигурацию из файла appsettings.json
var config = builder.Build();
var connectionString = config.GetConnectionString("DefaultConnection");// получаем строку подключения

var optionsBuilder1 = new DbContextOptionsBuilder<DbUserContext>();
var options1 = optionsBuilder1.UseSqlite(connectionString).Options;
using (DbUserContext db = new DbUserContext(options1))
{
    var Users = await db.Users.ToListAsync();

    if (Users.Count == 0)
    {
        User user1 = new User { Name = "Sasha", Age = 23 };
        User user2 = new User { Name = "Bob", Age = 24 };

        await db.AddRangeAsync(user1, user2);
        await db.SaveChangesAsync();

        Users = await db.Users.ToListAsync();
    }

    foreach (var item in Users)
    {
        Console.WriteLine($"{item.Id}-{item.Name}-{item.Age}");
    }
    Console.Read();
}