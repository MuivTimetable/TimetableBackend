using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimetableAPI.Migrations
{
    public partial class AddNewUserMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "User_id", "AuthCode", "Email", "Group_id", "Login", "Name", "Password", "Permission_id", "Token" },
                values: new object[] { 4, null, "70140101@online.muiv.ru", 1000018364, "70140101", "Александр Лаптев", "Qwe123asd", 2, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "User_id",
                keyValue: 4);
        }
    }
}
