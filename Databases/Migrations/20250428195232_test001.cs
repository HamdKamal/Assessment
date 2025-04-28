using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Databases.Migrations
{
    /// <inheritdoc />
    public partial class test001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Base64",
                table: "Sections",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Sections",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Filetype",
                table: "Sections",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Base64",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "Filetype",
                table: "Sections");
        }
    }
}
