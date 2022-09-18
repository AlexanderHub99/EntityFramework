using TPT_Table_Per_Type.Mobel;

Console.WriteLine("Hello, World!");

using (ApplicationContext db = new ApplicationContext())
{
    User user1 = new User { Name = "Tom" };
    User user2 = new User { Name = "Bob" };
    db.Users.Add(user1);
    db.Users.Add(user2);

    Employee employee = new Employee { Name = "Sam", Salary = 500 };
    db.Employees.Add(employee);

    Manager manager = new Manager { Name = "Robert", Departament = "IT" };
    db.Managers.Add(manager);

    db.SaveChanges();

    var users = db.Users.ToList();
    Console.WriteLine("Все пользователи");
    foreach (var user in users)
    {
        Console.WriteLine(user.Name);
    }

    Console.WriteLine("\n Все работники");
    foreach (var emp in db.Employees.ToList())
    {
        Console.WriteLine(emp.Name);
    }

    Console.WriteLine("\nВсе менеджеры");
    foreach (var man in db.Managers.ToList())
    {
        Console.WriteLine(man.Name);
    }
}