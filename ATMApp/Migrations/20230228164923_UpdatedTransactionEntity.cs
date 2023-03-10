using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace ATMApp.Migrations
{
    public partial class UpdatedTransactionEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TimeStamp",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Transactions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Transactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<byte[]>(
                name: "TimeStamp",
                table: "Transactions",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Transactions",
                type: "datetime2",
                nullable: true);
        }
    }
}
