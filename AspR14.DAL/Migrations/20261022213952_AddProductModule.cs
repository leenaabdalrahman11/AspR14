using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspR14.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddProductModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductTranslation_Products_ProductId",
                table: "ProductTranslation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductTranslation",
                table: "ProductTranslation");

            migrationBuilder.RenameTable(
                name: "ProductTranslation",
                newName: "ProductTranslations");

            migrationBuilder.RenameIndex(
                name: "IX_ProductTranslation_ProductId",
                table: "ProductTranslations",
                newName: "IX_ProductTranslations_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductTranslations",
                table: "ProductTranslations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTranslations_Products_ProductId",
                table: "ProductTranslations",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductTranslations_Products_ProductId",
                table: "ProductTranslations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductTranslations",
                table: "ProductTranslations");

            migrationBuilder.RenameTable(
                name: "ProductTranslations",
                newName: "ProductTranslation");

            migrationBuilder.RenameIndex(
                name: "IX_ProductTranslations_ProductId",
                table: "ProductTranslation",
                newName: "IX_ProductTranslation_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductTranslation",
                table: "ProductTranslation",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTranslation_Products_ProductId",
                table: "ProductTranslation",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
