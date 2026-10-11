using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AuctionApp.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Auctions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    OwnerName = table.Column<string>(type: "longtext", nullable: false),
                    StartingPrice = table.Column<double>(type: "double", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auctions", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Bids",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    BidderName = table.Column<string>(type: "longtext", nullable: false),
                    Amount = table.Column<double>(type: "double", nullable: false),
                    TimePlaced = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    AuctionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bids", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bids_Auctions_AuctionId",
                        column: x => x.AuctionId,
                        principalTable: "Auctions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Auctions",
                columns: new[] { "Id", "Description", "EndDate", "OwnerName", "StartingPrice", "Title" },
                values: new object[] { -1, "En mycket fin vas från Ming-dynastin.", new DateTime(2026, 10, 15, 13, 11, 7, 800, DateTimeKind.Local).AddTicks(9420), "Kalle", 500.0, "Antik Vas" });

            migrationBuilder.InsertData(
                table: "Bids",
                columns: new[] { "Id", "Amount", "AuctionId", "BidderName", "TimePlaced" },
                values: new object[,]
                {
                    { -2, 750.0, -1, "Oskar", new DateTime(2026, 10, 10, 12, 11, 7, 800, DateTimeKind.Local).AddTicks(9530) },
                    { -1, 600.0, -1, "Anna", new DateTime(2026, 10, 10, 11, 11, 7, 800, DateTimeKind.Local).AddTicks(9530) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bids_AuctionId",
                table: "Bids",
                column: "AuctionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bids");

            migrationBuilder.DropTable(
                name: "Auctions");
        }
    }
}
