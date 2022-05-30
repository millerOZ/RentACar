using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentACar.Migrations
{
    public partial class reserva : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reserves_Vehicles_VehicleId",
                table: "Reserves");

            migrationBuilder.DropIndex(
                name: "IX_Reserves_VehicleId",
                table: "Reserves");

            migrationBuilder.DropColumn(
                name: "VehicleId",
                table: "Reserves");

            migrationBuilder.CreateTable(
                name: "ReserveDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReserveId = table.Column<int>(type: "int", nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReserveDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReserveDetails_Reserves_ReserveId",
                        column: x => x.ReserveId,
                        principalTable: "Reserves",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReserveDetails_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReserveDetails_ReserveId",
                table: "ReserveDetails",
                column: "ReserveId");

            migrationBuilder.CreateIndex(
                name: "IX_ReserveDetails_VehicleId",
                table: "ReserveDetails",
                column: "VehicleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReserveDetails");

            migrationBuilder.AddColumn<int>(
                name: "VehicleId",
                table: "Reserves",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reserves_VehicleId",
                table: "Reserves",
                column: "VehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reserves_Vehicles_VehicleId",
                table: "Reserves",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
