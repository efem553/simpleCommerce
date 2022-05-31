using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace simpleCommerce_DataAccess.Migrations
{
    public partial class CartItemModelProductAndCountAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "CartItem",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "CartItem",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_ProductId",
                table: "CartItem",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_Product_ProductId",
                table: "CartItem",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_Product_ProductId",
                table: "CartItem");

            migrationBuilder.DropIndex(
                name: "IX_CartItem_ProductId",
                table: "CartItem");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "CartItem");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "CartItem");
        }
    }
}
