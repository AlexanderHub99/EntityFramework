using DatabaseSchemaManagementAndMigration.Model;
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
    var Car = await db.Car.ToListAsync();

    if (Users.Count == 0 && Car.Count == 0)
    {
        Car car1 = new Car{ Name = "Polo" , UserId = 1};
        Car car2 = new Car{ Name = "Polo" , UserId = 2};

        User user1 = new User { Name = "Bob", Age = 24, CarId = 1 , IsMarried = true , Position = "sad" };
        User user2 = new User { Name = "Sasha", Age = 23 ,  CarId = 2 , IsMarried = true , Position = "sad"};

        await db.Users.AddRangeAsync(user1, user2);
        await db.Car.AddRangeAsync(car1, car2);
        await db.SaveChangesAsync();

        Users = await db.Users.ToListAsync();
    }

    foreach (var item in Users)
    {
        Console.WriteLine($"Id:{item.Id}-Name:{item.Name}-Age:{item.Age}-CarId:{item.CarId}\nМашина -CarName:{item.Car?.Name}" +
            $"-UserId:{item.Car?.UserId}-UserName{item.Car?.User?.Name}");
    }
    Console.Read();
}