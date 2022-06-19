using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimetableAPI.Migrations
{
    public partial class AddScheduler_User_TotalizerMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Scheduler_User_Totalizers",
                columns: table => new
                {
                    Scheduler_id = table.Column<int>(type: "int", nullable: false),
                    User_id = table.Column<int>(type: "int", nullable: false),
                    TotalizerOption = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scheduler_User_Totalizers", x => new { x.Scheduler_id, x.User_id });
                    table.ForeignKey(
                        name: "FK_Scheduler_User_Totalizers_Schedulers_Scheduler_id",
                        column: x => x.Scheduler_id,
                        principalTable: "Schedulers",
                        principalColumn: "Scheduler_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Scheduler_User_Totalizers_Users_User_id",
                        column: x => x.User_id,
                        principalTable: "Users",
                        principalColumn: "User_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Scheduler_User_Totalizers_User_id",
                table: "Scheduler_User_Totalizers",
                column: "User_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Scheduler_User_Totalizers");
        }
    }
}
