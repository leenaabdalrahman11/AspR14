using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspR14.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddAuditLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryTranslations_Categories_CategoryId",
                table: "CategoryTranslations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "auditableEntities");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "auditableEntities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "auditableEntities",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "auditableEntities",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UpdatedById",
                table: "auditableEntities",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "auditableEntities",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_auditableEntities",
                table: "auditableEntities",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_auditableEntities_CreatedById",
                table: "auditableEntities",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_auditableEntities_UpdatedById",
                table: "auditableEntities",
                column: "UpdatedById");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropIndex(
                name: "IX_auditableEntities_CreatedById",
                table: "auditableEntities");

            migrationBuilder.DropIndex(
                name: "IX_auditableEntities_UpdatedById",
                table: "auditableEntities");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "auditableEntities");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "auditableEntities");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "auditableEntities");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "auditableEntities");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "auditableEntities");

            migrationBuilder.RenameTable(
                name: "auditableEntities",
                newName: "Categories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryTranslations_Categories_CategoryId",
                table: "CategoryTranslations",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
