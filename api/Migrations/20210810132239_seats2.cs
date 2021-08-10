using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class seats2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seats_SeatTypes_SeatTypeName",
                table: "Seats");

            migrationBuilder.DropIndex(
                name: "IX_Seats_SeatTypeName",
                table: "Seats");

            migrationBuilder.DropColumn(
                name: "SeatTypeName",
                table: "Seats");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Seats",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Seats_Type",
                table: "Seats",
                column: "Type");

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_SeatTypes_Type",
                table: "Seats",
                column: "Type",
                principalTable: "SeatTypes",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seats_SeatTypes_Type",
                table: "Seats");

            migrationBuilder.DropIndex(
                name: "IX_Seats_Type",
                table: "Seats");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Seats",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeatTypeName",
                table: "Seats",
                type: "nvarchar(450)",
                nullable: true);

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
        }
    }
}
