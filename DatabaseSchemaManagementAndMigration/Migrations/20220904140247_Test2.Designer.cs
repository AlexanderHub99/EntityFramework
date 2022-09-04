﻿// <auto-generated />
using DatabaseSchemaManagementAndMigration.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DatabaseSchemaManagementAndMigration.Migrations
{
    [DbContext(typeof(DbUserContext))]
    [Migration("20220904140247_Test2")]
    partial class Test2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.8");

            modelBuilder.Entity("DatabaseSchemaManagementAndMigration.Model.Car", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<long>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Car");
                });

            modelBuilder.Entity("DatabaseSchemaManagementAndMigration.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Age")
                        .HasColumnType("INTEGER");

                    b.Property<long>("CarId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsMarried")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Position")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CarId")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DatabaseSchemaManagementAndMigration.Model.User", b =>
                {
                    b.HasOne("DatabaseSchemaManagementAndMigration.Model.Car", "Car")
                        .WithOne("User")
                        .HasForeignKey("DatabaseSchemaManagementAndMigration.Model.User", "CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");
                });

            modelBuilder.Entity("DatabaseSchemaManagementAndMigration.Model.Car", b =>
                {
                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}