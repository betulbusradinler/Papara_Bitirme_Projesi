using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpenseTracker.Api.Migrations
{
    /// <inheritdoc />
    public partial class CreatePersonnelPasswordTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonnelPhones_Personnels_PersonnelId",
                table: "PersonnelPhones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonnelPhones",
                table: "PersonnelPhones");

            migrationBuilder.RenameTable(
                name: "PersonnelPhones",
                newName: "PersonnelPhone");

            migrationBuilder.RenameIndex(
                name: "IX_PersonnelPhones_PersonnelId",
                table: "PersonnelPhone",
                newName: "IX_PersonnelPhone_PersonnelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonnelPhone",
                table: "PersonnelPhone",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "PersonnelPasswords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonnelId = table.Column<int>(type: "int", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    CreatedUser = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    UpdatedUser = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonnelPasswords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonnelPasswords_Personnels_PersonnelId",
                        column: x => x.PersonnelId,
                        principalTable: "Personnels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonnelPasswords_PersonnelId",
                table: "PersonnelPasswords",
                column: "PersonnelId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonnelPhone_Personnels_PersonnelId",
                table: "PersonnelPhone",
                column: "PersonnelId",
                principalTable: "Personnels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonnelPhone_Personnels_PersonnelId",
                table: "PersonnelPhone");

            migrationBuilder.DropTable(
                name: "PersonnelPasswords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonnelPhone",
                table: "PersonnelPhone");

            migrationBuilder.RenameTable(
                name: "PersonnelPhone",
                newName: "PersonnelPhones");

            migrationBuilder.RenameIndex(
                name: "IX_PersonnelPhone_PersonnelId",
                table: "PersonnelPhones",
                newName: "IX_PersonnelPhones_PersonnelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonnelPhones",
                table: "PersonnelPhones",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonnelPhones_Personnels_PersonnelId",
                table: "PersonnelPhones",
                column: "PersonnelId",
                principalTable: "Personnels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
