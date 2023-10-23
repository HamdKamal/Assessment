using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Databases.Migrations
{
    /// <inheritdoc />
    public partial class mig0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Image",
                table: "Employees",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "ID",
                keyValue: new Guid("98d053d0-ab0d-4510-ac10-32d29189a301"),
                column: "NameEn",
                value: "HR Department");

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "ID",
                keyValue: new Guid("98d053d0-ab0d-4510-ac10-32d29189a302"),
                column: "NameEn",
                value: "financial Department ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "ID",
                keyValue: new Guid("98d053d0-ab0d-4510-ac10-32d29189a301"),
                column: "NameEn",
                value: "Human Resource Department");

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "ID",
                keyValue: new Guid("98d053d0-ab0d-4510-ac10-32d29189a302"),
                column: "NameEn",
                value: "financial Management Department ");
        }
    }
}
