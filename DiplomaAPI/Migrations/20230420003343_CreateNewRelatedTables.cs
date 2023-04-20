using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiplomaAPI.Migrations
{
    /// <inheritdoc />
    public partial class CreateNewRelatedTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Users_UserId",
                table: "UserRoles");

            migrationBuilder.DropIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "ChemSubstanceName",
                table: "DocFiles");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "DocFiles");

            migrationBuilder.RenameColumn(
                name: "Registered",
                table: "Users",
                newName: "UpdatedDatetime");

            migrationBuilder.RenameColumn(
                name: "RoleName",
                table: "UserRoles",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "Author",
                table: "DocFiles",
                newName: "Path");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDatetime",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UserRoleId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "UserRoles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "DocFiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Agreed",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AgreedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AgreedNumber = table.Column<int>(type: "int", nullable: false),
                    AgreedSinceDate = table.Column<DateTime>(type: "Date", nullable: false),
                    CreatedDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DocFileId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agreed", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Agreed_DocFiles_DocFileId",
                        column: x => x.DocFileId,
                        principalTable: "DocFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Approved",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApprovedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApprovedOrder = table.Column<int>(type: "int", nullable: false),
                    ApprovedSince = table.Column<DateTime>(type: "Date", nullable: false),
                    ApprovedNumber = table.Column<int>(type: "int", nullable: false),
                    CreatedDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DocFileId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Approved", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Approved_DocFiles_DocFileId",
                        column: x => x.DocFileId,
                        principalTable: "DocFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserRoleId",
                table: "Users",
                column: "UserRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_DocFiles_UserId",
                table: "DocFiles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Agreed_DocFileId",
                table: "Agreed",
                column: "DocFileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Approved_DocFileId",
                table: "Approved",
                column: "DocFileId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DocFiles_Users_UserId",
                table: "DocFiles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserRoles_UserRoleId",
                table: "Users",
                column: "UserRoleId",
                principalTable: "UserRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocFiles_Users_UserId",
                table: "DocFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserRoles_UserRoleId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Agreed");

            migrationBuilder.DropTable(
                name: "Approved");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserRoleId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_DocFiles_UserId",
                table: "DocFiles");

            migrationBuilder.DropColumn(
                name: "CreatedDatetime",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserRoleId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "DocFiles");

            migrationBuilder.RenameColumn(
                name: "UpdatedDatetime",
                table: "Users",
                newName: "Registered");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "UserRoles",
                newName: "RoleName");

            migrationBuilder.RenameColumn(
                name: "Path",
                table: "DocFiles",
                newName: "Author");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UserRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ChemSubstanceName",
                table: "DocFiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "DocFiles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoles",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Users_UserId",
                table: "UserRoles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
