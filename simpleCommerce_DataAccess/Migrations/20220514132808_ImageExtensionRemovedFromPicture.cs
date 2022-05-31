using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace simpleCommerce_DataAccess.Migrations
{
    public partial class ImageExtensionRemovedFromPicture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageExtension",
                table: "Picture");

            migrationBuilder.AlterColumn<string>(
                name: "ImageData",
                table: "Picture",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(byte[]),
                oldType: "BLOB",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "ImageData",
                table: "Picture",
                type: "BLOB",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "ImageExtension",
                table: "Picture",
                type: "TEXT",
                nullable: true);
        }
    }
}
