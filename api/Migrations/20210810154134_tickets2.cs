using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class tickets2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Seats_SeatId",
                table: "Ticket");

            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_SessionSeatPrices_SessionSeatPriceId",
                table: "Ticket");

            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Users_UserId",
                table: "Ticket");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ticket",
                table: "Ticket");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_SeatId",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "SeatId",
                table: "Ticket");

            migrationBuilder.RenameTable(
                name: "Ticket",
                newName: "Tickets");

            migrationBuilder.RenameIndex(
                name: "IX_Ticket_UserId",
                table: "Tickets",
                newName: "IX_Tickets_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Ticket_SessionSeatPriceId",
                table: "Tickets",
                newName: "IX_Tickets_SessionSeatPriceId");

            migrationBuilder.AddColumn<Guid>(
                name: "SessionServiceId",
                table: "Tickets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tickets",
                table: "Tickets",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_SessionServiceId",
                table: "Tickets",
                column: "SessionServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_SessionSeatPrices_SessionSeatPriceId",
                table: "Tickets",
                column: "SessionSeatPriceId",
                principalTable: "SessionSeatPrices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_SessionServices_SessionServiceId",
                table: "Tickets",
                column: "SessionServiceId",
                principalTable: "SessionServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Users_UserId",
                table: "Tickets",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_SessionSeatPrices_SessionSeatPriceId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_SessionServices_SessionServiceId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Users_UserId",
                table: "Tickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tickets",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_SessionServiceId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "SessionServiceId",
                table: "Tickets");

            migrationBuilder.RenameTable(
                name: "Tickets",
                newName: "Ticket");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_UserId",
                table: "Ticket",
                newName: "IX_Ticket_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_SessionSeatPriceId",
                table: "Ticket",
                newName: "IX_Ticket_SessionSeatPriceId");

            migrationBuilder.AddColumn<Guid>(
                name: "SeatId",
                table: "Ticket",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ticket",
                table: "Ticket",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_SeatId",
                table: "Ticket",
                column: "SeatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Seats_SeatId",
                table: "Ticket",
                column: "SeatId",
                principalTable: "Seats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_SessionSeatPrices_SessionSeatPriceId",
                table: "Ticket",
                column: "SessionSeatPriceId",
                principalTable: "SessionSeatPrices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Users_UserId",
                table: "Ticket",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
