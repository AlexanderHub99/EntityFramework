using ComplexTypes.Model;

Console.WriteLine("Hello, World!");

using (ApplicationContext db = new ApplicationContext())
{
    User user1 = new User("login1", "pass1234", new UserProfile { Age = 23, Name = "Tom" });
    User user2 = new User("login2","5678word2", new UserProfile { Age = 27, Name = "Alice" });
    db.Users.AddRange(user1, user2);
    db.SaveChanges();

    var users = db.Users.ToList();
    foreach (User user in users)
        Console.WriteLine(user.ToString());
}