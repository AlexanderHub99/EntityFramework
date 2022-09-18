﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TPT_Table_Per_Type.Mobel;

#nullable disable

namespace TPT_Table_Per_Type.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.9");

            modelBuilder.Entity("TPT_Table_Per_Type.Mobel.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TPT_Table_Per_Type.Mobel.Employee", b =>
                {
                    b.HasBaseType("TPT_Table_Per_Type.Mobel.User");

                    b.Property<int>("Salary")
                        .HasColumnType("INTEGER");

                    b.ToTable("Employees", (string)null);
                });

            modelBuilder.Entity("TPT_Table_Per_Type.Mobel.Manager", b =>
                {
                    b.HasBaseType("TPT_Table_Per_Type.Mobel.User");

                    b.Property<string>("Departament")
                        .HasColumnType("TEXT");

                    b.ToTable("Managers", (string)null);
                });

            modelBuilder.Entity("TPT_Table_Per_Type.Mobel.Employee", b =>
                {
                    b.HasOne("TPT_Table_Per_Type.Mobel.User", null)
                        .WithOne()
                        .HasForeignKey("TPT_Table_Per_Type.Mobel.Employee", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TPT_Table_Per_Type.Mobel.Manager", b =>
                {
                    b.HasOne("TPT_Table_Per_Type.Mobel.User", null)
                        .WithOne()
                        .HasForeignKey("TPT_Table_Per_Type.Mobel.Manager", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
