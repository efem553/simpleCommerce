using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace simpleCommerce_DataAccess.Migrations
{
    public partial class AboutModelArticle2Removed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Article2Content",
                table: "About");

            migrationBuilder.DropColumn(
                name: "Article2Header",
                table: "About");

            migrationBuilder.DropColumn(
                name: "Article2LogoBase64",
                table: "About");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Article2Content",
                table: "About",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Article2Header",
                table: "About",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Article2LogoBase64",
                table: "About",
                type: "TEXT",
                nullable: true);
        }
    }
}
