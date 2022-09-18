using ComplexTypes.Model;

Console.WriteLine("Hello, World!");

// добавление данных
using (ApplicationContext db = new ApplicationContext())
{
    User user1 = new User
    {
        Login = "login1",
        Password = "pass1234",
        Profile = new UserProfile { Age = 23, Name = "Tom" }
    };
    User user2 = new User
    {
        Login = "login2",
        Password = "5678word2",
        Profile = new UserProfile { Age = 27, Name = "Alice" }
    };

    db.Users.AddRange(user1, user2);
    db.SaveChanges();

    // добавление данных
    var users = db.Users.ToList();
    foreach (User u in users)
        Console.WriteLine($"Name: {u.Profile?.Name}  Age: {u.Profile?.Age}  Login: {u.Login} Password: {u.Password} ");
}