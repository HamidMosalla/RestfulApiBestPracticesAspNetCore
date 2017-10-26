using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetCoreWebApi.Migrations
{
    public partial class AddAddress2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Campers_CamperId",
                table: "Addresses");

            migrationBuilder.AlterColumn<int>(
                name: "CamperId",
                table: "Addresses",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Campers_CamperId",
                table: "Addresses",
                column: "CamperId",
                principalTable: "Campers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Campers_CamperId",
                table: "Addresses");

            migrationBuilder.AlterColumn<int>(
                name: "CamperId",
                table: "Addresses",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Campers_CamperId",
                table: "Addresses",
                column: "CamperId",
                principalTable: "Campers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
