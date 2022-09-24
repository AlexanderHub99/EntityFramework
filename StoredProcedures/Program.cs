using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StoredProcedures.Model;

Console.WriteLine("Hello, World!");


using (ApplicationContext db = new ApplicationContext())
{

    // Добавление данных 
    /* Company Microsoft = new Company{ Name = "Microsoft"};
     Company WorkSpes = new Company{ Name = "WorkSpes"};
     db.Companies.AddRange(Microsoft, WorkSpes);

     User tom = new User{ Name ="Tom" , Age = 23 , Company = Microsoft };
     User bob = new User{ Name ="Bob" , Age = 11 , Company = WorkSpes };
     User sasha = new User{ Name ="Sasha" , Age = 42 , Company = Microsoft };
     db.Users.AddRange(tom, bob, sasha);

     db.SaveChanges();*/

    SqlParameter param = new("@name", "Microsoft");
    var users = db.Users.FromSqlRaw("GetUsersByCompany @name", param).ToList();
    foreach (var p in users)
    {
        Console.WriteLine($"{p.Name} - {p.Age}");
    }
}

using (ApplicationContext db = new ApplicationContext())
{
    SqlParameter param = new()
    {
        ParameterName = "@userName",
        SqlDbType = System.Data.SqlDbType.VarChar,
        Direction = System.Data.ParameterDirection.Output,
        Size = 50
    };
    db.Database.ExecuteSqlRaw("GetUserWithMaxAge @userName OUT", param);
    Console.WriteLine(param.Value);
}