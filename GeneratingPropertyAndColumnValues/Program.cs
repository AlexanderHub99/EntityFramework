using GeneratingPropertyAndColumnValues.Model;

Console.WriteLine("Hello, World!");

// По умолчанию для свойств первичных ключей, которые представляют типы int или GUID и которые имеют значение
// по умолчанию, генерируется значение при вставке в базу данных. Для всех остальных свойств значения по
// умолчанию не генерируется.
using (ApplicationContext db = new())
{
    User user = new User { Name = "Tom" };
    Console.WriteLine($"Id перед добавлением в контекст {user.Id}");    // Id = 0
    db.Users.Add(user);
    db.SaveChanges();
    Console.WriteLine($"Id после добавления в базу данных {user.Id}");  // Id = 1 
}

// В этом случае, если мы не укажем значение для свойства Age, то ему будет присвоено значение 18:
// Авто значение в ApplicationContext
using (ApplicationContext db = new())    // На уровне базы данных это будет проявляться в установке
{                                                           // параметра DEFAULT:
    User user1 = new User() { Name = "Tom"};                // CREATE TABLE "Users" (
    Console.WriteLine($"Age: {user1.Age}"); // 0      // "Id"    INTEGER NOT NULL,
                                            // "Name"  TEXT,
    db.Users.Add(user1);                                    // "Age"   INTEGER NOT NULL DEFAULT 18,
    db.SaveChanges();                                       // CONSTRAINT "PK_Users" PRIMARY KEY("Id" AUTOINCREMENT)
                                                            // );
    Console.WriteLine($"Age: {user1.Age}"); // 18     //
}