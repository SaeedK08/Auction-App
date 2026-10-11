using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionApp.Migrations
{
    /// <inheritdoc />
    public partial class AmountToInt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Amount",
                table: "Bids",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<int>(
                name: "StartingPrice",
                table: "Auctions",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "EndDate", "StartingPrice" },
                values: new object[] { new DateTime(2026, 10, 15, 20, 47, 23, 470, DateTimeKind.Local).AddTicks(3620), 500 });

            migrationBuilder.UpdateData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: -2,
                columns: new[] { "Amount", "TimePlaced" },
                values: new object[] { 750, new DateTime(2026, 10, 10, 19, 47, 23, 470, DateTimeKind.Local).AddTicks(3770) });

            migrationBuilder.UpdateData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "Amount", "TimePlaced" },
                values: new object[] { 600, new DateTime(2026, 10, 10, 18, 47, 23, 470, DateTimeKind.Local).AddTicks(3770) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Amount",
                table: "Bids",
                type: "double",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "StartingPrice",
                table: "Auctions",
                type: "double",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Auctions",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "EndDate", "StartingPrice" },
                values: new object[] { new DateTime(2026, 10, 15, 13, 11, 7, 800, DateTimeKind.Local).AddTicks(9420), 500.0 });

            migrationBuilder.UpdateData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: -2,
                columns: new[] { "Amount", "TimePlaced" },
                values: new object[] { 750.0, new DateTime(2026, 10, 10, 12, 11, 7, 800, DateTimeKind.Local).AddTicks(9530) });

            migrationBuilder.UpdateData(
                table: "Bids",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "Amount", "TimePlaced" },
                values: new object[] { 600.0, new DateTime(2026, 10, 10, 11, 11, 7, 800, DateTimeKind.Local).AddTicks(9530) });
        }
    }
}
