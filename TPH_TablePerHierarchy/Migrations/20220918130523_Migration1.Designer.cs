﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TPH_TablePerHierarchy.Model;

#nullable disable

namespace TPH_TablePerHierarchy.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20220918130523_Migration1")]
    partial class Migration1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.9");

            modelBuilder.Entity("TPH_TablePerHierarchy.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");
                });

            modelBuilder.Entity("TPH_TablePerHierarchy.Model.Employee", b =>
                {
                    b.HasBaseType("TPH_TablePerHierarchy.Model.User");

                    b.Property<int>("Salary")
                        .HasColumnType("INTEGER");

                    b.HasDiscriminator().HasValue("Employee");
                });

            modelBuilder.Entity("TPH_TablePerHierarchy.Model.Manager", b =>
                {
                    b.HasBaseType("TPH_TablePerHierarchy.Model.User");

                    b.Property<string>("Departament")
                        .HasColumnType("TEXT");

                    b.HasDiscriminator().HasValue("Manager");
                });
#pragma warning restore 612, 618
        }
    }
}
