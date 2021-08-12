using Microsoft.EntityFrameworkCore.Migrations;

namespace e_commerce.Migrations
{
    public partial class eleven : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderReview_Customer_UserId",
                table: "OrderReview");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "OrderReview",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_OrderReview_UserId",
                table: "OrderReview",
                newName: "IX_OrderReview_UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderReview_Customer_UserID",
                table: "OrderReview",
                column: "UserID",
                principalTable: "Customer",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderReview_Customer_UserID",
                table: "OrderReview");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "OrderReview",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderReview_UserID",
                table: "OrderReview",
                newName: "IX_OrderReview_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderReview_Customer_UserId",
                table: "OrderReview",
                column: "UserId",
                principalTable: "Customer",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
