using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace simpleCommerce_DataAccess.Migrations
{
    public partial class AboutModelAndFacultyLogoModelsAreAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "About",
                columns: table => new
                {
                    AboutArticleId = table.Column<Guid>(type: "TEXT", nullable: false),
                    BaseIconBase64 = table.Column<string>(type: "TEXT", nullable: false),
                    StoreName = table.Column<string>(type: "TEXT", nullable: false),
                    Article1Header = table.Column<string>(type: "TEXT", nullable: false),
                    Article1Content = table.Column<string>(type: "TEXT", nullable: false),
                    Article2Header = table.Column<string>(type: "TEXT", nullable: false),
                    Article2LogoBase64 = table.Column<string>(type: "TEXT", nullable: false),
                    Article2Content = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_About", x => x.AboutArticleId);
                });

            migrationBuilder.CreateTable(
                name: "FacultyLogo",
                columns: table => new
                {
                    FacultyLogoId = table.Column<Guid>(type: "TEXT", nullable: false),
                    LogoBase64 = table.Column<string>(type: "TEXT", nullable: false),
                    FacultyName = table.Column<string>(type: "TEXT", nullable: false),
                    AboutArticleId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacultyLogo", x => x.FacultyLogoId);
                    table.ForeignKey(
                        name: "FK_FacultyLogo_About_AboutArticleId",
                        column: x => x.AboutArticleId,
                        principalTable: "About",
                        principalColumn: "AboutArticleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FacultyLogo_AboutArticleId",
                table: "FacultyLogo",
                column: "AboutArticleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FacultyLogo");

            migrationBuilder.DropTable(
                name: "About");
        }
    }
}
