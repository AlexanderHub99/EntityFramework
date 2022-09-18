using HierarchicalData.Model;

Console.WriteLine("Hello, World!");

using (ApplicationContext db = new ApplicationContext())
{
    // пересоздаем бд
    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();

    // добавляем начальные данные
    MenuItem file = new MenuItem { Title = "File" };
    MenuItem edit = new MenuItem { Title = "Edit" };
    MenuItem open = new MenuItem { Title = "Open", Parent = file };
    MenuItem save = new MenuItem { Title = "Save", Parent = file };

    MenuItem copy = new MenuItem { Title = "Copy", Parent = edit };
    MenuItem paste = new MenuItem { Title = "Paste", Parent = edit };

    db.MenuItems.AddRange(file, edit, open, save, copy, paste);
    db.SaveChanges();
}
using (ApplicationContext db = new ApplicationContext())
{
    // получаем все пункты меню из бд
    var menuItems = db.MenuItems.ToList();
    Console.WriteLine("All Menu:");
    foreach (MenuItem m in menuItems)
    {
        Console.WriteLine(m.Title);
    }
    Console.WriteLine();
    // получаем определенный пункт меню с подменю
    var fileMenu = db.MenuItems.FirstOrDefault(m => m.Title == "File");
    if (fileMenu != null)
    {
        Console.WriteLine(fileMenu.Title);
        foreach (var m in fileMenu.Children)
        {
            Console.WriteLine($"---{m.Title}");
        }
    }
}