using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Grigorovici_Tonia_Lab2.Migrations
{
    public partial class Borrowings1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Borrowing_BookID",
                table: "Borrowing");

            migrationBuilder.AddColumn<int>(
                name: "BorrowingID",
                table: "Book",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Borrowing_BookID",
                table: "Borrowing",
                column: "BookID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Borrowing_BookID",
                table: "Borrowing");

            migrationBuilder.DropColumn(
                name: "BorrowingID",
                table: "Book");

            migrationBuilder.CreateIndex(
                name: "IX_Borrowing_BookID",
                table: "Borrowing",
                column: "BookID");
        }
    }
}
