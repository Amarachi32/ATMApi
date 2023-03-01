using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ATMApp.Migrations
{
    public partial class UpdatedTransactionkey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Users_UserAccountId",
                table: "Transactions");

            migrationBuilder.AlterColumn<int>(
                name: "UserAccountId",
                table: "Transactions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Users_UserAccountId",
                table: "Transactions",
                column: "UserAccountId",
                principalTable: "Users",
                principalColumn: "UserAccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Users_UserAccountId",
                table: "Transactions");

            migrationBuilder.AlterColumn<int>(
                name: "UserAccountId",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Users_UserAccountId",
                table: "Transactions",
                column: "UserAccountId",
                principalTable: "Users",
                principalColumn: "UserAccountId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
