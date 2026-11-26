using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspR14.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddImageUploadIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MainImageUploadId",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SubImageUploadIds",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainImageUploadId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SubImageUploadIds",
                table: "Products");
        }
    }
}
