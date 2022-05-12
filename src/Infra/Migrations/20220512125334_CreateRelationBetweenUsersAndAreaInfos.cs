using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    public partial class CreateRelationBetweenUsersAndAreaInfos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<ulong>(
                name: "AreaInfoId",
                table: "users",
                type: "bigint(20) unsigned",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_AreaInfoId",
                table: "users",
                column: "AreaInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_users_area_info_AreaInfoId",
                table: "users",
                column: "AreaInfoId",
                principalTable: "area_info",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_area_info_AreaInfoId",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_AreaInfoId",
                table: "users");

            migrationBuilder.DropColumn(
                name: "AreaInfoId",
                table: "users");
        }
    }
}
