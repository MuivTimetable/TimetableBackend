using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimetableAPI.Migrations
{
    public partial class AddTestUsersMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "User_id", "AuthCode", "Email", "Group_id", "Login", "Name", "Password", "Permission_id", "Token", "preToken" },
                values: new object[] { 5, null, "70140101@online.muiv.ru", 1111, "test1", "Test1", "test1", 2, null, null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "User_id", "AuthCode", "Email", "Group_id", "Login", "Name", "Password", "Permission_id", "Token", "preToken" },
                values: new object[] { 6, null, "70139904@online.muiv.ru", 1111, "test2", "Test2", "test2", 2, null, null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "User_id", "AuthCode", "Email", "Group_id", "Login", "Name", "Password", "Permission_id", "Token", "preToken" },
                values: new object[] { 7, null, "70134928@online.muiv.ru", 1111, "test3", "Test3", "test3", 2, null, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "User_id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "User_id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "User_id",
                keyValue: 7);
        }
    }
}
