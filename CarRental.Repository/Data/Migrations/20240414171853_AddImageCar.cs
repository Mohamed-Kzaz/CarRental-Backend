using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRental.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddImageCar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CarImageURL",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarImageURL",
                table: "Cars");
        }
    }
}
