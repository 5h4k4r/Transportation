using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    public partial class CreateRelationBetweenLangugagesAndUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_users_language_id",
                table: "users",
                column: "language_id");

            migrationBuilder.AddForeignKey(
                name: "FK_users_languages_language_id",
                table: "users",
                column: "language_id",
                principalTable: "languages",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_languages_language_id",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_language_id",
                table: "users");
        }
    }
}
