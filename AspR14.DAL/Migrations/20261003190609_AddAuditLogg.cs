using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspR14.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddAuditLogg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_auditableEntities_AspNetUsers_CreatedById",
                table: "auditableEntities");

            migrationBuilder.DropForeignKey(
                name: "FK_auditableEntities_AspNetUsers_UpdatedById",
                table: "auditableEntities");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryTranslations_auditableEntities_CategoryId",
                table: "CategoryTranslations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_auditableEntities",
                table: "auditableEntities");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "auditableEntities");

            migrationBuilder.RenameTable(
                name: "auditableEntities",
                newName: "Categories");

            migrationBuilder.RenameIndex(
                name: "IX_auditableEntities_UpdatedById",
                table: "Categories",
                newName: "IX_Categories_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_auditableEntities_CreatedById",
                table: "Categories",
                newName: "IX_Categories_CreatedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_AspNetUsers_CreatedById",
                table: "Categories",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_AspNetUsers_UpdatedById",
                table: "Categories",
                column: "UpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryTranslations_Categories_CategoryId",
                table: "CategoryTranslations",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_AspNetUsers_CreatedById",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_AspNetUsers_UpdatedById",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryTranslations_Categories_CategoryId",
                table: "CategoryTranslations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "auditableEntities");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_UpdatedById",
                table: "auditableEntities",
                newName: "IX_auditableEntities_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_CreatedById",
                table: "auditableEntities",
                newName: "IX_auditableEntities_CreatedById");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "auditableEntities",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_auditableEntities",
                table: "auditableEntities",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_auditableEntities_AspNetUsers_CreatedById",
                table: "auditableEntities",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_auditableEntities_AspNetUsers_UpdatedById",
                table: "auditableEntities",
                column: "UpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryTranslations_auditableEntities_CategoryId",
                table: "CategoryTranslations",
                column: "CategoryId",
                principalTable: "auditableEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
