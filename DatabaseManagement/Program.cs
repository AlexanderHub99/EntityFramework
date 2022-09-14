using DatabaseManagement;
using DatabaseManagement.Models;

Console.WriteLine("Hello, World!");

using (ApplicationContext context = new())
{
    var isCreated = context.Database.EnsureCreated();
    Console.WriteLine(Script.IsCreated(isCreated));

    var isDelete = context.Database.EnsureDeleted();
    Console.WriteLine(Script.IsDelete(isDelete));

    // асинхронная версия
    isCreated = await context.Database.EnsureCreatedAsync();
    Console.WriteLine(Script.IsCreated(isCreated));

    // асинхронная версия
    isDelete = await context.Database.EnsureDeletedAsync();
    Console.WriteLine(Script.IsDelete(isDelete));
}
