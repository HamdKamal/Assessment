using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Databases.Migrations
{
    /// <inheritdoc />
    public partial class test23 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Primaries",
                columns: table => new
                {
                    Ref_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrgenaztionId = table.Column<int>(type: "int", nullable: true),
                    RegionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsApproval = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Primaries", x => x.Ref_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    Ref_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsOpen = table.Column<bool>(type: "bit", nullable: true),
                    IsComplete = table.Column<bool>(type: "bit", nullable: true),
                    TBL_PrimaryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.Ref_ID);
                    table.ForeignKey(
                        name: "FK_Sections_Primaries_TBL_PrimaryId",
                        column: x => x.TBL_PrimaryId,
                        principalTable: "Primaries",
                        principalColumn: "Ref_Id");
                });

            migrationBuilder.CreateTable(
                name: "Fields",
                columns: table => new
                {
                    Ref_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaselineValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaselineDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TargetValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TargetDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ChangePercentage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SectionRef_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fields", x => x.Ref_ID);
                    table.ForeignKey(
                        name: "FK_Fields_Sections_SectionRef_ID",
                        column: x => x.SectionRef_ID,
                        principalTable: "Sections",
                        principalColumn: "Ref_ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fields_SectionRef_ID",
                table: "Fields",
                column: "SectionRef_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_TBL_PrimaryId",
                table: "Sections",
                column: "TBL_PrimaryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fields");

            migrationBuilder.DropTable(
                name: "Sections");

            migrationBuilder.DropTable(
                name: "Primaries");
        }
    }
}
