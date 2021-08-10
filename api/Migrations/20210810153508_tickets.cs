using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class tickets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Role",
                table: "Users");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Roles_Name",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Roles");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Users",
                newName: "RoleName");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Role",
                table: "Users",
                newName: "IX_Users_RoleName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "Name");

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    SessionSeatPriceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SeatId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ticket_Seats_SeatId",
                        column: x => x.SeatId,
                        principalTable: "Seats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ticket_SessionSeatPrices_SessionSeatPriceId",
                        column: x => x.SessionSeatPriceId,
                        principalTable: "SessionSeatPrices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ticket_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_SeatId",
                table: "Ticket",
                column: "SeatId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_SessionSeatPriceId",
                table: "Ticket",
                column: "SessionSeatPriceId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_UserId",
                table: "Ticket",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleName",
                table: "Users",
                column: "RoleName",
                principalTable: "Roles",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleName",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.RenameColumn(
                name: "RoleName",
                table: "Users",
                newName: "Role");

            migrationBuilder.RenameIndex(
                name: "IX_Users_RoleName",
                table: "Users",
                newName: "IX_Users_Role");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Roles",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Roles_Name",
                table: "Roles",
                column: "Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Role",
                table: "Users",
                column: "Role",
                principalTable: "Roles",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
