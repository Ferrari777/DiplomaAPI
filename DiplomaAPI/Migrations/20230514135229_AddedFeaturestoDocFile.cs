using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiplomaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedFeaturestoDocFile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Path",
                table: "DocFiles");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "DocFiles",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "DocFiles",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "FileSize",
                table: "DocFiles",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "DocFiles");

            migrationBuilder.DropColumn(
                name: "FileSize",
                table: "DocFiles");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "DocFiles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "DocFiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
