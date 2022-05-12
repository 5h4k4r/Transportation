using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    public partial class CreateRelationBetweenEmployeesAndAreaInfos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<ulong>(
                name: "AreaInfoId",
                table: "employees",
                type: "bigint(20) unsigned",
                nullable: false,
                defaultValue: 0ul);

            migrationBuilder.CreateIndex(
                name: "IX_employees_AreaInfoId",
                table: "employees",
                column: "AreaInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_employees_area_info_AreaInfoId",
                table: "employees",
                column: "AreaInfoId",
                principalTable: "area_info",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employees_area_info_AreaInfoId",
                table: "employees");

            migrationBuilder.DropIndex(
                name: "IX_employees_AreaInfoId",
                table: "employees");

            migrationBuilder.DropColumn(
                name: "AreaInfoId",
                table: "employees");
        }
    }
}
