using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetCoreWebApi.Migrations
{
    public partial class AddAddress1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_Campers_CamperId",
                table: "Address");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Address",
                table: "Address");

            migrationBuilder.RenameTable(
                name: "Address",
                newName: "Addresses");

            migrationBuilder.RenameIndex(
                name: "IX_Address_CamperId",
                table: "Addresses",
                newName: "IX_Addresses_CamperId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Campers_CamperId",
                table: "Addresses",
                column: "CamperId",
                principalTable: "Campers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Campers_CamperId",
                table: "Addresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses");

            migrationBuilder.RenameTable(
                name: "Addresses",
                newName: "Address");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_CamperId",
                table: "Address",
                newName: "IX_Address_CamperId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Address",
                table: "Address",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Campers_CamperId",
                table: "Address",
                column: "CamperId",
                principalTable: "Campers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
