using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiplomaAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixBugInContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DocFile",
                table: "DocFile");

            migrationBuilder.RenameTable(
                name: "DocFile",
                newName: "DocFiles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DocFiles",
                table: "DocFiles",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DocFiles",
                table: "DocFiles");

            migrationBuilder.RenameTable(
                name: "DocFiles",
                newName: "DocFile");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DocFile",
                table: "DocFile",
                column: "Id");
        }
    }
}
