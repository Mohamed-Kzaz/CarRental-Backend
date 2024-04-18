using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRental.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class DeleteTwoTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarRentalDamages");

            migrationBuilder.DropTable(
                name: "DamageReports");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DamageReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dam_Desc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Repair_Cost = table.Column<int>(type: "int", nullable: false),
                    Report_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DamageReports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarRentalDamages",
                columns: table => new
                {
                    RentId = table.Column<int>(type: "int", nullable: false),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    DmgId = table.Column<int>(type: "int", nullable: false),
                    DamageReportId = table.Column<int>(type: "int", nullable: false),
                    RentalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarRentalDamages", x => new { x.RentId, x.CarId, x.DmgId });
                    table.ForeignKey(
                        name: "FK_CarRentalDamages_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarRentalDamages_DamageReports_DamageReportId",
                        column: x => x.DamageReportId,
                        principalTable: "DamageReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarRentalDamages_Rentals_RentalId",
                        column: x => x.RentalId,
                        principalTable: "Rentals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarRentalDamages_CarId",
                table: "CarRentalDamages",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_CarRentalDamages_DamageReportId",
                table: "CarRentalDamages",
                column: "DamageReportId");

            migrationBuilder.CreateIndex(
                name: "IX_CarRentalDamages_RentalId",
                table: "CarRentalDamages",
                column: "RentalId");
        }
    }
}
