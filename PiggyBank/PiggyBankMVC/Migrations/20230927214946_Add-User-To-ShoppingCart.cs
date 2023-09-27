using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PiggyBankMVC.Migrations
{
    /// <inheritdoc />
    public partial class AddUserToShoppingCart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "Identity",
                table: "ShoppingCarts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_UserId",
                schema: "Identity",
                table: "ShoppingCarts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_AspNetUsers_UserId",
                schema: "Identity",
                table: "ShoppingCarts",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_AspNetUsers_UserId",
                schema: "Identity",
                table: "ShoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCarts_UserId",
                schema: "Identity",
                table: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "Identity",
                table: "ShoppingCarts");
        }
    }
}
