using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentACar.Migrations
{
    public partial class editModelRental : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rental_Reserves_ReserveId",
                table: "Rental");

            migrationBuilder.DropForeignKey(
                name: "FK_RentalType_Rental_RentalId",
                table: "RentalType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rental",
                table: "Rental");

            migrationBuilder.RenameTable(
                name: "Rental",
                newName: "Rentals");

            migrationBuilder.RenameIndex(
                name: "IX_Rental_ReserveId",
                table: "Rentals",
                newName: "IX_Rentals_ReserveId");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentType",
                table: "Rentals",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rentals",
                table: "Rentals",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_Name_ReserveId",
                table: "Rentals",
                columns: new[] { "Name", "ReserveId" },
                unique: true,
                filter: "[ReserveId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Reserves_ReserveId",
                table: "Rentals",
                column: "ReserveId",
                principalTable: "Reserves",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RentalType_Rentals_RentalId",
                table: "RentalType",
                column: "RentalId",
                principalTable: "Rentals",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Reserves_ReserveId",
                table: "Rentals");

            migrationBuilder.DropForeignKey(
                name: "FK_RentalType_Rentals_RentalId",
                table: "RentalType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rentals",
                table: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_Name_ReserveId",
                table: "Rentals");

            migrationBuilder.RenameTable(
                name: "Rentals",
                newName: "Rental");

            migrationBuilder.RenameIndex(
                name: "IX_Rentals_ReserveId",
                table: "Rental",
                newName: "IX_Rental_ReserveId");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentType",
                table: "Rental",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rental",
                table: "Rental",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rental_Reserves_ReserveId",
                table: "Rental",
                column: "ReserveId",
                principalTable: "Reserves",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RentalType_Rental_RentalId",
                table: "RentalType",
                column: "RentalId",
                principalTable: "Rental",
                principalColumn: "Id");
        }
    }
}
