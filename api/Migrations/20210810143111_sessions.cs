using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class sessions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seats_SeatTypes_Type",
                table: "Seats");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Seats",
                newName: "SeatType");

            migrationBuilder.RenameIndex(
                name: "IX_Seats_Type",
                table: "Seats",
                newName: "IX_Seats_SeatType");

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HallId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CinemaMovieId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sessions_CinemaMovies_CinemaMovieId",
                        column: x => x.CinemaMovieId,
                        principalTable: "CinemaMovies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sessions_Halls_HallId",
                        column: x => x.HallId,
                        principalTable: "Halls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SessionSeatPrice",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SeatType = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Price = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionSeatPrice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SessionSeatPrice_SeatTypes_SeatType",
                        column: x => x.SeatType,
                        principalTable: "SeatTypes",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SessionSeatPrice_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_CinemaMovieId",
                table: "Sessions",
                column: "CinemaMovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_HallId",
                table: "Sessions",
                column: "HallId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionSeatPrice_SeatType",
                table: "SessionSeatPrice",
                column: "SeatType");

            migrationBuilder.CreateIndex(
                name: "IX_SessionSeatPrice_SessionId",
                table: "SessionSeatPrice",
                column: "SessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_SeatTypes_SeatType",
                table: "Seats",
                column: "SeatType",
                principalTable: "SeatTypes",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seats_SeatTypes_SeatType",
                table: "Seats");

            migrationBuilder.DropTable(
                name: "SessionSeatPrice");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.RenameColumn(
                name: "SeatType",
                table: "Seats",
                newName: "Type");

            migrationBuilder.RenameIndex(
                name: "IX_Seats_SeatType",
                table: "Seats",
                newName: "IX_Seats_Type");

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_SeatTypes_Type",
                table: "Seats",
                column: "Type",
                principalTable: "SeatTypes",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
