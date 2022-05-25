using Microsoft.EntityFrameworkCore.Migrations;

namespace RescueSite.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Users_AdminId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_AdminId",
                table: "Requests");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Requests_AdminId",
                table: "Requests",
                column: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Users_AdminId",
                table: "Requests",
                column: "AdminId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
