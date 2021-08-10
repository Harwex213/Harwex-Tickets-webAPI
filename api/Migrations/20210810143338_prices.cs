using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class prices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SessionSeatPrice_SeatTypes_SeatType",
                table: "SessionSeatPrice");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionSeatPrice_Sessions_SessionId",
                table: "SessionSeatPrice");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SessionSeatPrice",
                table: "SessionSeatPrice");

            migrationBuilder.RenameTable(
                name: "SessionSeatPrice",
                newName: "SessionSeatPrices");

            migrationBuilder.RenameIndex(
                name: "IX_SessionSeatPrice_SessionId",
                table: "SessionSeatPrices",
                newName: "IX_SessionSeatPrices_SessionId");

            migrationBuilder.RenameIndex(
                name: "IX_SessionSeatPrice_SeatType",
                table: "SessionSeatPrices",
                newName: "IX_SessionSeatPrices_SeatType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SessionSeatPrices",
                table: "SessionSeatPrices",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SessionSeatPrices_SeatTypes_SeatType",
                table: "SessionSeatPrices",
                column: "SeatType",
                principalTable: "SeatTypes",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionSeatPrices_Sessions_SessionId",
                table: "SessionSeatPrices",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SessionSeatPrices_SeatTypes_SeatType",
                table: "SessionSeatPrices");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionSeatPrices_Sessions_SessionId",
                table: "SessionSeatPrices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SessionSeatPrices",
                table: "SessionSeatPrices");

            migrationBuilder.RenameTable(
                name: "SessionSeatPrices",
                newName: "SessionSeatPrice");

            migrationBuilder.RenameIndex(
                name: "IX_SessionSeatPrices_SessionId",
                table: "SessionSeatPrice",
                newName: "IX_SessionSeatPrice_SessionId");

            migrationBuilder.RenameIndex(
                name: "IX_SessionSeatPrices_SeatType",
                table: "SessionSeatPrice",
                newName: "IX_SessionSeatPrice_SeatType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SessionSeatPrice",
                table: "SessionSeatPrice",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SessionSeatPrice_SeatTypes_SeatType",
                table: "SessionSeatPrice",
                column: "SeatType",
                principalTable: "SeatTypes",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionSeatPrice_Sessions_SessionId",
                table: "SessionSeatPrice",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
