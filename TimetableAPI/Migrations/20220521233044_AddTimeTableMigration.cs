using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimetableAPI.Migrations
{
    public partial class AddTimeTableMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Group_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Group_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Group_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Permission_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Permission_name = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Permission_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Report_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    User_id = table.Column<int>(type: "int", nullable: true),
                    Message = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    report_date = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Report_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SchedulerDates",
                columns: table => new
                {
                    Day_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Work_Year = table.Column<int>(type: "int", nullable: false),
                    Work_Month = table.Column<int>(type: "int", nullable: false),
                    Work_Day = table.Column<int>(type: "int", nullable: false),
                    Work_Date_Name = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchedulerDates", x => x.Day_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    User_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Login = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Group_id = table.Column<int>(type: "int", nullable: true),
                    Token = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AuthCode = table.Column<int>(type: "int", nullable: true),
                    Permission_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.User_id);
                    table.ForeignKey(
                        name: "FK_Users_Groups_Group_id",
                        column: x => x.Group_id,
                        principalTable: "Groups",
                        principalColumn: "Group_id");
                    table.ForeignKey(
                        name: "FK_Users_Permissions_Permission_id",
                        column: x => x.Permission_id,
                        principalTable: "Permissions",
                        principalColumn: "Permission_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Schedulers",
                columns: table => new
                {
                    Scheduler_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Day_id = table.Column<int>(type: "int", nullable: false),
                    Branch = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Work_start = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Work_end = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Area = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Work_type = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Place = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Tutor = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Cathedra = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Comment = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Totalizer = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedulers", x => x.Scheduler_id);
                    table.ForeignKey(
                        name: "FK_Schedulers_SchedulerDates_Day_id",
                        column: x => x.Day_id,
                        principalTable: "SchedulerDates",
                        principalColumn: "Day_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Schedulers_Groups",
                columns: table => new
                {
                    Scheduler_id = table.Column<int>(type: "int", nullable: false),
                    Group_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedulers_Groups", x => new { x.Scheduler_id, x.Group_id });
                    table.ForeignKey(
                        name: "FK_Schedulers_Groups_Groups_Group_id",
                        column: x => x.Group_id,
                        principalTable: "Groups",
                        principalColumn: "Group_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Schedulers_Groups_Schedulers_Scheduler_id",
                        column: x => x.Scheduler_id,
                        principalTable: "Schedulers",
                        principalColumn: "Scheduler_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Group_id", "Group_name" },
                values: new object[,]
                {
                    { 1000017945, "о.ЭЗДт 32.2/Б-20" },
                    { 1000018011, "о.ИЗДт 30.2/Б1-20" },
                    { 1000018210, "л.ЭЗДт 32.1/Б1-20" },
                    { 1000018364, "л.ЮВДтс 22.1/Б2-20" },
                    { 1000019061, "з.ЮЗДт 82.3/М2-20" },
                    { 1000019464, "РЮД 13.1-21" },
                    { 1000019466, "РЭД 21.1-21" },
                    { 1000019467, "РЭД 20.1-21" },
                    { 1000019558, "о.УЗДт 21.2/Б6-20" },
                    { 1000020418, "о.УЗДт 21.2/Б7-20" }
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Permission_id", "Permission_name" },
                values: new object[,]
                {
                    { 1, "Студент" },
                    { 2, "Староста или помощник" },
                    { 3, "Преподаватель" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "User_id", "AuthCode", "Email", "Group_id", "Login", "Name", "Password", "Permission_id", "Token" },
                values: new object[] { 1, null, "70134928@online.muiv.ru", 1000017945, "robpol", "Роберт Полсон", "qwerty", 2, null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "User_id", "AuthCode", "Email", "Group_id", "Login", "Name", "Password", "Permission_id", "Token" },
                values: new object[] { 2, null, "70137919@online.muiv.ru", 1000018364, "70137919", "Артур Пендрагон", "ZCj,frfNsCj,frf", 1, null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "User_id", "AuthCode", "Email", "Group_id", "Login", "Name", "Password", "Permission_id", "Token" },
                values: new object[] { 3, null, "70139904@online.muiv.ru", 1000017945, "1111", "Олегг", "qwert", 2, null });

            migrationBuilder.CreateIndex(
                name: "IX_Schedulers_Day_id",
                table: "Schedulers",
                column: "Day_id");

            migrationBuilder.CreateIndex(
                name: "IX_Schedulers_Groups_Group_id",
                table: "Schedulers_Groups",
                column: "Group_id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Group_id",
                table: "Users",
                column: "Group_id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Permission_id",
                table: "Users",
                column: "Permission_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "Schedulers_Groups");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Schedulers");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "SchedulerDates");
        }
    }
}
