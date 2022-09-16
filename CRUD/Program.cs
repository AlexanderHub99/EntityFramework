using CRUD.Models;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");

// Добавление
using (ApplicationContext db = new())
{
    User tom1 = new() { Name = "Tom", Age = 33, Femali = "Ferstan" };
    User tom2 = new() { Name = "Tom1", Age = 3324, Femali = "Ferstan1" };
    User tom3 = new() { Name = "Tom3", Age = 32343, Femali = "Ferstan3" };
    User alice1 = new() { Name = "Alice", Age = 26, Femali = "Robinsen" };
    User alice2 = new() { Name = "Alice2", Age = 236, Femali = "Robinsen2" };
    User alice3 = new() { Name = "Alice3", Age = 926, Femali = "Robinsen3" };

    // Добавление
    await db.Users.AddRangeAsync(tom1, tom2, tom3, alice1, alice2, alice3);
    await db.SaveChangesAsync();
}

// получение
using (ApplicationContext db = new())
{
    // получаем объекты из бд и выводим на консоль
    var users = await db.Users.ToListAsync();
    Console.WriteLine("Данные после добавления:");
    foreach (User u in users)
    {
        Console.WriteLine($"{u.Id}.{u.Name} -{u.Femali}- {u.Age}");
    }
}

// Редактирование
using (ApplicationContext db = new())
{
    // получаем первый объект
    User? user = await db.Users.FirstOrDefaultAsync();
    if (user != null)
    {
        user.Name = "Bob";
        user.Age = 44;
        //обновляем объект
        await db.SaveChangesAsync();
    }

    // выводим данные после обновления
    Console.WriteLine("\nДанные после редактирования:");
    var users = await db.Users.ToListAsync();
    foreach (User u in users)
    {
        Console.WriteLine($"{u.Id}.{u.Name} e -{u.Femali}- {u.Age}");
    }
}

// Удаление
using (ApplicationContext db = new())
{
    // получаем первый объект
    User? user1 = await db.Users.FirstOrDefaultAsync();
    User? user2 = await db.Users.FirstOrDefaultAsync(u => u.Id == 2);
    User? user3 = await db.Users.FirstOrDefaultAsync(u => u.Id == 3);

    if (user1 != null && user2 != null && user3 != null)
    {
        //удаляем объект
        db.Users.Remove(user3);
        db.Users.RemoveRange(user1, user2);
        await db.SaveChangesAsync();
    }

    // выводим данные после обновления
    Console.WriteLine("\nДанные после удаления:");
    var users = await db.Users.ToListAsync();
    foreach (User u in users)
    {
        Console.WriteLine($"{u.Id}.{u.Name} -{u.Femali}- {u.Age}");
    }
}