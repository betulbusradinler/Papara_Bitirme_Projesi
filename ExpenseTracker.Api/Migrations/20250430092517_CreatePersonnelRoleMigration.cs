using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpenseTracker.Api.Migrations
{
    /// <inheritdoc />
    public partial class CreatePersonnelRoleMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonnelPasswords_Personnels_PersonnelId",
                table: "PersonnelPasswords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonnelPasswords",
                table: "PersonnelPasswords");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Personnels");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Personnels");

            migrationBuilder.DropColumn(
                name: "Secret",
                table: "Personnels");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "PersonnelPasswords");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "PersonnelPasswords");

            migrationBuilder.RenameTable(
                name: "PersonnelPasswords",
                newName: "PersonnelPassword");

            migrationBuilder.RenameIndex(
                name: "IX_PersonnelPasswords_PersonnelId",
                table: "PersonnelPassword",
                newName: "IX_PersonnelPassword_PersonnelId");

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "Personnels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "PersonnelPassword",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Secret",
                table: "PersonnelPassword",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonnelPassword",
                table: "PersonnelPassword",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "PersonnelRole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "50"),
                    CreatedUser = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    UpdatedUser = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonnelRole", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Personnels_RoleId",
                table: "Personnels",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonnelPassword_Personnels_PersonnelId",
                table: "PersonnelPassword",
                column: "PersonnelId",
                principalTable: "Personnels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Personnels_PersonnelRole_RoleId",
                table: "Personnels",
                column: "RoleId",
                principalTable: "PersonnelRole",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonnelPassword_Personnels_PersonnelId",
                table: "PersonnelPassword");

            migrationBuilder.DropForeignKey(
                name: "FK_Personnels_PersonnelRole_RoleId",
                table: "Personnels");

            migrationBuilder.DropTable(
                name: "PersonnelRole");

            migrationBuilder.DropIndex(
                name: "IX_Personnels_RoleId",
                table: "Personnels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonnelPassword",
                table: "PersonnelPassword");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Personnels");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "PersonnelPassword");

            migrationBuilder.DropColumn(
                name: "Secret",
                table: "PersonnelPassword");

            migrationBuilder.RenameTable(
                name: "PersonnelPassword",
                newName: "PersonnelPasswords");

            migrationBuilder.RenameIndex(
                name: "IX_PersonnelPassword_PersonnelId",
                table: "PersonnelPasswords",
                newName: "IX_PersonnelPasswords_PersonnelId");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Personnels",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Personnels",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Secret",
                table: "Personnels",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "PersonnelPasswords",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "PersonnelPasswords",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonnelPasswords",
                table: "PersonnelPasswords",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonnelPasswords_Personnels_PersonnelId",
                table: "PersonnelPasswords",
                column: "PersonnelId",
                principalTable: "Personnels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
