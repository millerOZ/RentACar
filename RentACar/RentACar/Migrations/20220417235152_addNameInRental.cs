using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentACar.Migrations
{
    public partial class addNameInRental : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rental_Reserves_ReserveId",
                table: "Rental");

            migrationBuilder.AlterColumn<int>(
                name: "ReserveId",
                table: "Rental",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Rental",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Rental_Reserves_ReserveId",
                table: "Rental",
                column: "ReserveId",
                principalTable: "Reserves",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rental_Reserves_ReserveId",
                table: "Rental");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Rental");

            migrationBuilder.AlterColumn<int>(
                name: "ReserveId",
                table: "Rental",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Rental_Reserves_ReserveId",
                table: "Rental",
                column: "ReserveId",
                principalTable: "Reserves",
                principalColumn: "Id");
        }
    }
}
