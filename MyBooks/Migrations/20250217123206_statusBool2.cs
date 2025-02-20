using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBooks.Migrations
{
    /// <inheritdoc />
    public partial class statusBool2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookStatus",
                table: "Borrowings");

            migrationBuilder.AddColumn<bool>(
                name: "Available",
                table: "Books",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Available",
                table: "Books");

            migrationBuilder.AddColumn<bool>(
                name: "BookStatus",
                table: "Borrowings",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
