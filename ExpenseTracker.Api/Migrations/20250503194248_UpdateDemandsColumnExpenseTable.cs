using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpenseTracker.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDemandsColumnExpenseTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Demand_DemandId",
                table: "Expenses");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_DemandId",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "DemandId",
                table: "Expenses");

            migrationBuilder.AddColumn<int>(
                name: "ExpenseId",
                table: "Demand",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Demand_ExpenseId",
                table: "Demand",
                column: "ExpenseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Demand_Expenses_ExpenseId",
                table: "Demand",
                column: "ExpenseId",
                principalTable: "Expenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Demand_Expenses_ExpenseId",
                table: "Demand");

            migrationBuilder.DropIndex(
                name: "IX_Demand_ExpenseId",
                table: "Demand");

            migrationBuilder.DropColumn(
                name: "ExpenseId",
                table: "Demand");

            migrationBuilder.AddColumn<int>(
                name: "DemandId",
                table: "Expenses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_DemandId",
                table: "Expenses",
                column: "DemandId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Demand_DemandId",
                table: "Expenses",
                column: "DemandId",
                principalTable: "Demand",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
