using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentACar.Migrations
{
    public partial class updateRentalType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentalType_Rentals_RentalId",
                table: "RentalType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RentalType",
                table: "RentalType");

            migrationBuilder.RenameTable(
                name: "RentalType",
                newName: "RentalTypes");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "RentalTypes",
                newName: "Name");

            migrationBuilder.RenameIndex(
                name: "IX_RentalType_RentalId",
                table: "RentalTypes",
                newName: "IX_RentalTypes_RentalId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RentalTypes",
                table: "RentalTypes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_RentalTypes_Name_RentalId",
                table: "RentalTypes",
                columns: new[] { "Name", "RentalId" },
                unique: true,
                filter: "[Name] IS NOT NULL AND [RentalId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_RentalTypes_Rentals_RentalId",
                table: "RentalTypes",
                column: "RentalId",
                principalTable: "Rentals",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentalTypes_Rentals_RentalId",
                table: "RentalTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RentalTypes",
                table: "RentalTypes");

            migrationBuilder.DropIndex(
                name: "IX_RentalTypes_Name_RentalId",
                table: "RentalTypes");

            migrationBuilder.RenameTable(
                name: "RentalTypes",
                newName: "RentalType");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "RentalType",
                newName: "Description");

            migrationBuilder.RenameIndex(
                name: "IX_RentalTypes_RentalId",
                table: "RentalType",
                newName: "IX_RentalType_RentalId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RentalType",
                table: "RentalType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RentalType_Rentals_RentalId",
                table: "RentalType",
                column: "RentalId",
                principalTable: "Rentals",
                principalColumn: "Id");
        }
    }
}
