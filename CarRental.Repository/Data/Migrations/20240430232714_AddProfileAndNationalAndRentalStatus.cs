using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRental.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddProfileAndNationalAndRentalStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pay_Date",
                table: "Rentals");

            migrationBuilder.RenameColumn(
                name: "Trans_Id",
                table: "Rentals",
                newName: "Status");

            migrationBuilder.AddColumn<string>(
                name: "ImageProfileURl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NationalIdURl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageProfileURl",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NationalIdURl",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Rentals",
                newName: "Trans_Id");

            migrationBuilder.AddColumn<DateTime>(
                name: "Pay_Date",
                table: "Rentals",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
