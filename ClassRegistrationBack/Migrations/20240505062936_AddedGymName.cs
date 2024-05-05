using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClassRegistrationBack.Migrations
{
    /// <inheritdoc />
    public partial class AddedGymName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "sections",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "instructors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "gyms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "sections");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "instructors");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "gyms");
        }
    }
}
