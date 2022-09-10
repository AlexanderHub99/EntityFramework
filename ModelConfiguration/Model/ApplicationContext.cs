using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModelConfiguration.Model.EntityTypeConfig;

namespace ModelConfiguration.Model
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;

        public DbSet<Child> Childs { get; set; } = null!;

        public DbSet<Kindergarten> Kindergartens { get; set; } = null!;

        public ApplicationContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=helloapp.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()                              // конфигурация для типа User
                .ToTable("People")
                .Property(p => p.Name)
                .IsRequired();

            modelBuilder.ApplyConfiguration(new UserConfiguration());

            modelBuilder.Entity<Kindergarten>(KindergartenConfigure); // конфигурация для типа Kindergarten

            // конфигурация для типа Child в одноименной модели 

            // Для инициализации БД при конфигурации определенной модели
            modelBuilder.Entity<User>()// вызывается метод HasData(), в который передаются добавляемые данные:
                .HasData(new User { Id = 1, Name = "Tom", Age = 36 });

            // Например, инициализируем БД набором данных:
            // Далее по цепочке вызывается метод HasData(), который собственно и определяет начальные данные.
            // В данном случае это набор из трех объектов User. При этом для каждого объекта необходимо установить
            // значение первичного ключа - в данном случае значение свойства Id. Причем вне зависимости, генерирует
            // ли база данных для данных автоматически индентификатор или нет, нам в любом случае его надо установить
            // - это основное ограничение при инициализации БД начальными данными.
            // При этом следует учитывать, что инициализация начальными данными будет выполняться только в двух случаях:
            // ----> При выполнении миграции. (При создании миграции добавляемые данные автоматически включаются в скрипт миграции)
            // ----> При вызове метода Database.EnsureCreated(), который создает БД при ее отсутствии
            modelBuilder.Entity<User>()
                .HasData(new User { Id = 2, Name = "Tom", Age = 23 },
                              new User { Id = 3, Name = "Alice", Age = 26 },
                              new User { Id = 4, Name = "Sam", Age = 28 });

            // Аналогично можно инициализировать данные нескольких сущностей, в том числе связанных между собой:
            // определяем садиков
            Kindergarten giraffe1 = new Kindergarten { Id = 1, Name = "giraffe" };
            Kindergarten giraffe2 = new Kindergarten { Id = 2, Name = "giraffe" };
            Kindergarten elephant = new Kindergarten { Id = 3, Name = "elephant" };
            // определяем детей
            Child tom = new Child { Id = 1, Name = "Tom", KindergartenId = giraffe1.Id };
            Child alice = new Child { Id = 2, Name = "Alice", KindergartenId = giraffe2.Id };
            Child sam = new Child { Id = 3, Name = "Sam", KindergartenId = elephant.Id };
            // определяем детей к садикам 
            giraffe1.ChildId = tom.Id;
            giraffe2.ChildId = alice.Id;
            elephant.ChildId = sam.Id;


            // добавляем данные для обеих сущностей
            modelBuilder.Entity<Kindergarten>().HasData(giraffe1, giraffe2, elephant);
            modelBuilder.Entity<User>().HasData(tom, alice, sam);

        }

        // конфигурация для типа Kindergarten
        public void KindergartenConfigure(EntityTypeBuilder<Kindergarten> builder)
        {
            builder.ToTable("Enterprises").Property(c => c.Name).IsRequired();
        }
    }
}
