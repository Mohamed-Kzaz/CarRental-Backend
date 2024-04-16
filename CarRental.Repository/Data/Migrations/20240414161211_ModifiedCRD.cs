using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRental.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedCRD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarRentalDamages_DamageReports_DamageReportId",
                table: "CarRentalDamages");

            migrationBuilder.DropForeignKey(
                name: "FK_CarRentalDamages_Rentals_RentalId",
                table: "CarRentalDamages");

            migrationBuilder.AlterColumn<int>(
                name: "RentalId",
                table: "CarRentalDamages",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DamageReportId",
                table: "CarRentalDamages",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CarRentalDamages_DamageReports_DamageReportId",
                table: "CarRentalDamages",
                column: "DamageReportId",
                principalTable: "DamageReports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarRentalDamages_Rentals_RentalId",
                table: "CarRentalDamages",
                column: "RentalId",
                principalTable: "Rentals",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarRentalDamages_DamageReports_DamageReportId",
                table: "CarRentalDamages");

            migrationBuilder.DropForeignKey(
                name: "FK_CarRentalDamages_Rentals_RentalId",
                table: "CarRentalDamages");

            migrationBuilder.AlterColumn<int>(
                name: "RentalId",
                table: "CarRentalDamages",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DamageReportId",
                table: "CarRentalDamages",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_CarRentalDamages_DamageReports_DamageReportId",
                table: "CarRentalDamages",
                column: "DamageReportId",
                principalTable: "DamageReports",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CarRentalDamages_Rentals_RentalId",
                table: "CarRentalDamages",
                column: "RentalId",
                principalTable: "Rentals",
                principalColumn: "Id");
        }
    }
}
