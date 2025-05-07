using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpenseTracker.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateExpenseRejectDescriptionColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RejectDescription",
                table: "Expenses",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "Reddedilme Mesajı Yok.",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "PersonnelPasswords",
                keyColumn: "Id",
                keyValue: -2,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 7, 4, 34, 41, 800, DateTimeKind.Utc).AddTicks(5330));

            migrationBuilder.UpdateData(
                table: "PersonnelPasswords",
                keyColumn: "Id",
                keyValue: -1,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 7, 4, 34, 41, 800, DateTimeKind.Utc).AddTicks(5320));

            migrationBuilder.UpdateData(
                table: "Personnels",
                keyColumn: "Id",
                keyValue: -2,
                columns: new[] { "CreatedDate", "OpenDate" },
                values: new object[] { new DateTime(2025, 5, 7, 7, 34, 41, 800, DateTimeKind.Local).AddTicks(5270), new DateTime(2025, 5, 7, 7, 34, 41, 800, DateTimeKind.Local).AddTicks(5270) });

            migrationBuilder.UpdateData(
                table: "Personnels",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "CreatedDate", "OpenDate" },
                values: new object[] { new DateTime(2025, 5, 7, 7, 34, 41, 800, DateTimeKind.Local).AddTicks(5260), new DateTime(2025, 5, 7, 7, 34, 41, 800, DateTimeKind.Local).AddTicks(5230) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RejectDescription",
                table: "Expenses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "Reddedilme Mesajı Yok.");

            migrationBuilder.UpdateData(
                table: "PersonnelPasswords",
                keyColumn: "Id",
                keyValue: -2,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 7, 3, 11, 23, 77, DateTimeKind.Utc).AddTicks(5790));

            migrationBuilder.UpdateData(
                table: "PersonnelPasswords",
                keyColumn: "Id",
                keyValue: -1,
                column: "CreatedDate",
                value: new DateTime(2025, 5, 7, 3, 11, 23, 77, DateTimeKind.Utc).AddTicks(5770));

            migrationBuilder.UpdateData(
                table: "Personnels",
                keyColumn: "Id",
                keyValue: -2,
                columns: new[] { "CreatedDate", "OpenDate" },
                values: new object[] { new DateTime(2025, 5, 7, 6, 11, 23, 77, DateTimeKind.Local).AddTicks(5730), new DateTime(2025, 5, 7, 6, 11, 23, 77, DateTimeKind.Local).AddTicks(5730) });

            migrationBuilder.UpdateData(
                table: "Personnels",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "CreatedDate", "OpenDate" },
                values: new object[] { new DateTime(2025, 5, 7, 6, 11, 23, 77, DateTimeKind.Local).AddTicks(5720), new DateTime(2025, 5, 7, 6, 11, 23, 77, DateTimeKind.Local).AddTicks(5690) });
        }
    }
}
