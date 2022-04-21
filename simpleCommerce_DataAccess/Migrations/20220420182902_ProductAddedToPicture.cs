using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace simpleCommerce_DataAccess.Migrations
{
    public partial class ProductAddedToPicture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Picture_Product_PictureId",
                table: "Picture");

            migrationBuilder.DropForeignKey(
                name: "FK_Property_Product_ProductId",
                table: "Property");

            migrationBuilder.DropIndex(
                name: "IX_Property_ProductId",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "PictureId",
                table: "Product");

            migrationBuilder.AlterColumn<byte[]>(
                name: "ImageData",
                table: "Picture",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "Picture",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Picture_ProductId",
                table: "Picture",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Picture_Product_ProductId",
                table: "Picture",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Property_Product_PropertyId",
                table: "Property",
                column: "PropertyId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Picture_Product_ProductId",
                table: "Picture");

            migrationBuilder.DropForeignKey(
                name: "FK_Property_Product_PropertyId",
                table: "Property");

            migrationBuilder.DropIndex(
                name: "IX_Picture_ProductId",
                table: "Picture");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Picture");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "Property",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PictureId",
                table: "Product",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<byte[]>(
                name: "ImageData",
                table: "Picture",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Property_ProductId",
                table: "Property",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Picture_Product_PictureId",
                table: "Picture",
                column: "PictureId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Property_Product_ProductId",
                table: "Property",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id");
        }
    }
}
