using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpenseTracker.Api.Migrations
{
    /// <inheritdoc />
    public partial class DeletePersonnelRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personnels_PersonnelRole_RoleId",
                table: "Personnels");

            migrationBuilder.DropTable(
                name: "PersonnelRole");

            migrationBuilder.DropIndex(
                name: "IX_Personnels_RoleId",
                table: "Personnels");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Personnels");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Personnels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Personnels");

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "Personnels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PersonnelRole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedUser = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "50"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedUser = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
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
                name: "FK_Personnels_PersonnelRole_RoleId",
                table: "Personnels",
                column: "RoleId",
                principalTable: "PersonnelRole",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
