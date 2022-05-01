using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentACar.Migrations
{
    public partial class licencetype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeLicence",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "LicenceTypeId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "LicenceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicenceTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_LicenceTypeId",
                table: "AspNetUsers",
                column: "LicenceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LicenceTypes_Name",
                table: "LicenceTypes",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_LicenceTypes_LicenceTypeId",
                table: "AspNetUsers",
                column: "LicenceTypeId",
                principalTable: "LicenceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_LicenceTypes_LicenceTypeId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "LicenceTypes");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_LicenceTypeId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LicenceTypeId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "TypeLicence",
                table: "AspNetUsers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }
    }
}
