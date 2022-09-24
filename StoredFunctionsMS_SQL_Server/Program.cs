
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StoredFunctionsMS_SQL_Server_.Model;

Console.WriteLine("Hello, World!");

using (ApplicationContext db = new())
{
    Company mi = new Company{  Name = "Mi"};
    Company mic = new Company{  Name = "Mi"};
    db.AddRange(mi, mic);

    User Bob = new User{ Age = 23, Name = "Bob", Company =mi };
    User tom = new User{ Age = 43, Name = "tom", Company =mic };
    User Anya = new User{ Age = 3, Name = "Anya", Company =mic };
    User Letha = new User{ Age = 54, Name = "Letha", Company =mi };
    User sasha = new User{ Age = 12, Name = "sasha", Company =mic };

    db.AddRange(Bob, tom, Anya, Letha, sasha);
    db.SaveChanges();
}

// Обращение к функции в запросе SQL
// Первый подход предполагает обращение к хранимой функции в запросе SQL, который отправляется из кода C#:
using (ApplicationContext db = new ApplicationContext())
{
    SqlParameter param = new SqlParameter("@age", 30);
    var users = db.Users.FromSqlRaw("SELECT * FROM GetUsersByAge (@age)", param).ToList();

    // В данном случае в запросе вместо таблицы указываем имя вызов функции с переданными ей параметрами: GetUsersByAge (@age)
    // В итоге результат данного запроса будет таким же, что и при выполнении скрипта выше.
    foreach (var u in users)
    {
        Console.WriteLine($"{u.Name} - {u.Age}");
    }
}


// Работа обновленного контекста базы данных
using (StoredFunctionsMS_SQL_Server_.Model.ChangedСontextСlass.ApplicationContext db = new())
{
    var users = db.GetUsersByAge(30);   // обращение к хранимой функции
    foreach (var u in users)
    {
        Console.WriteLine($"{u.Name} - {u.Age}");
    }
}
