using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpenseTracker.Api.Migrations
{
    /// <inheritdoc />
    public partial class DropDemandsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Demands_DemandId",
                table: "Expenses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Demands",
                table: "Demands");

            migrationBuilder.RenameTable(
                name: "Demands",
                newName: "Demand");

            migrationBuilder.AddColumn<int>(
                name: "Demand",
                table: "Expenses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Demand",
                table: "Demand",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Demand_DemandId",
                table: "Expenses",
                column: "DemandId",
                principalTable: "Demand",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Demand_DemandId",
                table: "Expenses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Demand",
                table: "Demand");

            migrationBuilder.DropColumn(
                name: "Demand",
                table: "Expenses");

            migrationBuilder.RenameTable(
                name: "Demand",
                newName: "Demands");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Demands",
                table: "Demands",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Demands_DemandId",
                table: "Expenses",
                column: "DemandId",
                principalTable: "Demands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
