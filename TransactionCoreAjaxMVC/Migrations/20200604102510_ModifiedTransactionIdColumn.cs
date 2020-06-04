using Microsoft.EntityFrameworkCore.Migrations;

namespace TransactionCoreAjaxMVC.Migrations
{
    public partial class ModifiedTransactionIdColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TransctionId",
                table: "Transactions",
                newName: "TransactionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TransactionId",
                table: "Transactions",
                newName: "TransctionId");
        }
    }
}
