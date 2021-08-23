using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastucture.Migrations
{
    public partial class RemoveSessionSeatPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_SessionSeatPrices_SessionSeatPriceId",
                table: "Tickets");

            migrationBuilder.DropTable(
                name: "SessionSeatPrices");

            migrationBuilder.RenameColumn(
                name: "SessionSeatPriceId",
                table: "Tickets",
                newName: "SessionId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_SessionSeatPriceId",
                table: "Tickets",
                newName: "IX_Tickets_SessionId");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Sessions",
                type: "money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Sessions_SessionId",
                table: "Tickets",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Sessions_SessionId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Sessions");

            migrationBuilder.RenameColumn(
                name: "SessionId",
                table: "Tickets",
                newName: "SessionSeatPriceId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_SessionId",
                table: "Tickets",
                newName: "IX_Tickets_SessionSeatPriceId");

            migrationBuilder.CreateTable(
                name: "SessionSeatPrices",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    SessionId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionSeatPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SessionSeatPrices_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SessionSeatPrices_SessionId",
                table: "SessionSeatPrices",
                column: "SessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_SessionSeatPrices_SessionSeatPriceId",
                table: "Tickets",
                column: "SessionSeatPriceId",
                principalTable: "SessionSeatPrices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
