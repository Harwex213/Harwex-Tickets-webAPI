using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastucture.Migrations
{
    public partial class removeSeatTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seats_SeatTypes_SeatTypeName",
                table: "Seats");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionSeatPrices_SeatTypes_SeatTypeName",
                table: "SessionSeatPrices");

            migrationBuilder.DropTable(
                name: "SeatTypes");

            migrationBuilder.DropIndex(
                name: "IX_SessionSeatPrices_SeatTypeName",
                table: "SessionSeatPrices");

            migrationBuilder.DropIndex(
                name: "IX_Seats_SeatTypeName",
                table: "Seats");

            migrationBuilder.DropColumn(
                name: "SessionServiceId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "SeatTypeName",
                table: "SessionSeatPrices");

            migrationBuilder.DropColumn(
                name: "SeatTypeName",
                table: "Seats");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "SessionServiceId",
                table: "Tickets",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "SeatTypeName",
                table: "SessionSeatPrices",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeatTypeName",
                table: "Seats",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SeatTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatTypes", x => x.Id);
                    table.UniqueConstraint("AK_SeatTypes_Name", x => x.Name);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SessionSeatPrices_SeatTypeName",
                table: "SessionSeatPrices",
                column: "SeatTypeName");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_SeatTypeName",
                table: "Seats",
                column: "SeatTypeName");

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_SeatTypes_SeatTypeName",
                table: "Seats",
                column: "SeatTypeName",
                principalTable: "SeatTypes",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionSeatPrices_SeatTypes_SeatTypeName",
                table: "SessionSeatPrices",
                column: "SeatTypeName",
                principalTable: "SeatTypes",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
