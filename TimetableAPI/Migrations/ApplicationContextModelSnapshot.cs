﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TimetableAPI;

#nullable disable

namespace TimetableAPI.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("TimetableAPI.Models.Group", b =>
                {
                    b.Property<int>("Group_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Group_name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Group_id");

                    b.ToTable("Groups");

                    b.HasData(
                        new
                        {
                            Group_id = 1111,
                            Group_name = "TEST"
                        });
                });

            modelBuilder.Entity("TimetableAPI.Models.Permission", b =>
                {
                    b.Property<int>("Permission_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Permission_name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.HasKey("Permission_id");

                    b.ToTable("Permissions");

                    b.HasData(
                        new
                        {
                            Permission_id = 1,
                            Permission_name = "Студент"
                        },
                        new
                        {
                            Permission_id = 2,
                            Permission_name = "Староста или помощник"
                        },
                        new
                        {
                            Permission_id = 3,
                            Permission_name = "Преподаватель"
                        });
                });

            modelBuilder.Entity("TimetableAPI.Models.Report", b =>
                {
                    b.Property<int>("Report_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.Property<int?>("User_id")
                        .HasColumnType("int");

                    b.Property<DateTime>("report_date")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Report_id");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("TimetableAPI.Models.Scheduler", b =>
                {
                    b.Property<int>("Scheduler_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Area")
                        .HasColumnType("longtext");

                    b.Property<string>("Branch")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Cathedra")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Comment")
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.Property<int>("Day_id")
                        .HasColumnType("int");

                    b.Property<string>("Place")
                        .HasColumnType("longtext");

                    b.Property<int>("Totalizer")
                        .HasColumnType("int");

                    b.Property<string>("Tutor")
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Work_end")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)");

                    b.Property<string>("Work_start")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)");

                    b.Property<string>("Work_type")
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.HasKey("Scheduler_id");

                    b.HasIndex("Day_id");

                    b.ToTable("Schedulers");
                });

            modelBuilder.Entity("TimetableAPI.Models.Scheduler_Group", b =>
                {
                    b.Property<int>("Scheduler_id")
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<int>("Group_id")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.HasKey("Scheduler_id", "Group_id");

                    b.HasIndex("Group_id");

                    b.ToTable("Schedulers_Groups");
                });

            modelBuilder.Entity("TimetableAPI.Models.SchedulerDate", b =>
                {
                    b.Property<int>("Day_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Work_Date_Name")
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<int>("Work_Day")
                        .HasColumnType("int");

                    b.Property<int>("Work_Month")
                        .HasColumnType("int");

                    b.Property<int>("Work_Year")
                        .HasColumnType("int");

                    b.HasKey("Day_id");

                    b.ToTable("SchedulerDates");
                });

            modelBuilder.Entity("TimetableAPI.Models.Session", b =>
                {
                    b.Property<int>("User_id")
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<string>("SessionIdentificator")
                        .HasColumnType("varchar(255)")
                        .HasColumnOrder(1);

                    b.HasKey("User_id", "SessionIdentificator");

                    b.ToTable("Session");
                });

            modelBuilder.Entity("TimetableAPI.Models.User", b =>
                {
                    b.Property<int>("User_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("AuthCode")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("Group_id")
                        .HasColumnType("int");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("Permission_id")
                        .HasColumnType("int");

                    b.Property<string>("Token")
                        .HasColumnType("longtext");

                    b.Property<string>("preToken")
                        .HasColumnType("longtext");

                    b.HasKey("User_id");

                    b.HasIndex("Group_id");

                    b.HasIndex("Permission_id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            User_id = 1,
                            Email = "70134928@online.muiv.ru",
                            Group_id = 1000017945,
                            Login = "robpol",
                            Name = "Роберт Полсон",
                            Password = "qwerty",
                            Permission_id = 2
                        },
                        new
                        {
                            User_id = 2,
                            Email = "70137919@online.muiv.ru",
                            Group_id = 1000018364,
                            Login = "70137919",
                            Name = "Артур Пендрагон",
                            Password = "ZCj,frfNsCj,frf",
                            Permission_id = 1
                        },
                        new
                        {
                            User_id = 3,
                            Email = "70139904@online.muiv.ru",
                            Group_id = 1000017945,
                            Login = "1111",
                            Name = "Олегг",
                            Password = "qwert",
                            Permission_id = 2
                        },
                        new
                        {
                            User_id = 4,
                            Email = "70140101@online.muiv.ru",
                            Group_id = 1000018364,
                            Login = "70140101",
                            Name = "Александр Лаптев",
                            Password = "Qwe123asd",
                            Permission_id = 2
                        });
                });

            modelBuilder.Entity("TimetableAPI.Models.Scheduler", b =>
                {
                    b.HasOne("TimetableAPI.Models.SchedulerDate", "SchedulerDate")
                        .WithMany()
                        .HasForeignKey("Day_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SchedulerDate");
                });

            modelBuilder.Entity("TimetableAPI.Models.Scheduler_Group", b =>
                {
                    b.HasOne("TimetableAPI.Models.Group", "Group")
                        .WithMany()
                        .HasForeignKey("Group_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TimetableAPI.Models.Scheduler", "Scheduler")
                        .WithMany()
                        .HasForeignKey("Scheduler_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("Scheduler");
                });

            modelBuilder.Entity("TimetableAPI.Models.Session", b =>
                {
                    b.HasOne("TimetableAPI.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("User_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("TimetableAPI.Models.User", b =>
                {
                    b.HasOne("TimetableAPI.Models.Group", "Group")
                        .WithMany()
                        .HasForeignKey("Group_id");

                    b.HasOne("TimetableAPI.Models.Permission", "Permission")
                        .WithMany()
                        .HasForeignKey("Permission_id");

                    b.Navigation("Group");

                    b.Navigation("Permission");
                });
#pragma warning restore 612, 618
        }
    }
}
