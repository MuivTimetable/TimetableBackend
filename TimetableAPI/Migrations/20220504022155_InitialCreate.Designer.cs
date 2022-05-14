﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TimetableAPI;

#nullable disable

namespace TimetableAPI.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20220504022155_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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
                });

            modelBuilder.Entity("TimetableAPI.Models.Permission", b =>
                {
                    b.Property<int>("Permission_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Permission_name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.HasKey("Permission_id");

                    b.ToTable("Permissions");
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

                    b.Property<string>("Cathedra")
                        .HasColumnType("longtext");

                    b.Property<string>("Comment")
                        .HasColumnType("longtext");

                    b.Property<int>("Day_id")
                        .HasColumnType("int");

                    b.Property<int>("Group_id")
                        .HasColumnType("int");

                    b.Property<string>("Place")
                        .HasColumnType("longtext");

                    b.Property<int>("Totalizer")
                        .HasColumnType("int");

                    b.Property<string>("Tutor")
                        .HasColumnType("longtext");

                    b.Property<string>("Work_end")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Work_start")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Work_type")
                        .HasColumnType("longtext");

                    b.HasKey("Scheduler_id");

                    b.HasIndex("Day_id");

                    b.HasIndex("Group_id");

                    b.ToTable("Schedulers");
                });

            modelBuilder.Entity("TimetableAPI.Models.Scheduler_Group", b =>
                {
                    b.Property<int>("Scheduler_id")
                        .HasColumnType("int");

                    b.Property<int>("Group_id")
                        .HasColumnType("int");

                    b.HasKey("Scheduler_id");

                    b.HasIndex("Group_id");

                    b.ToTable("Schedulers_Groups");
                });

            modelBuilder.Entity("TimetableAPI.Models.SchedulerDate", b =>
                {
                    b.Property<int>("Day_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Work_day")
                        .HasColumnType("int");

                    b.HasKey("Day_id");

                    b.ToTable("SchedulerDates");
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
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("Permission_id")
                        .HasColumnType("int");

                    b.Property<string>("Token")
                        .HasColumnType("longtext");

                    b.HasKey("User_id");

                    b.HasIndex("Group_id");

                    b.HasIndex("Permission_id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TimetableAPI.Models.Scheduler", b =>
                {
                    b.HasOne("TimetableAPI.Models.SchedulerDate", "SchedulerDate")
                        .WithMany()
                        .HasForeignKey("Day_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TimetableAPI.Models.Group", "Group")
                        .WithMany()
                        .HasForeignKey("Group_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

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