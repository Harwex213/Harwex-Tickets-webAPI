using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class privarykey_cinemamovies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CinemaMovies_Cinemas_CinemaId1",
                table: "CinemaMovies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CinemaMovies",
                table: "CinemaMovies");

            migrationBuilder.DropIndex(
                name: "IX_CinemaMovies_CinemaId1",
                table: "CinemaMovies");

            migrationBuilder.RenameColumn(
                name: "CinemaId1",
                table: "CinemaMovies",
                newName: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CinemaMovies",
                table: "CinemaMovies",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CinemaMovies_CinemaId",
                table: "CinemaMovies",
                column: "CinemaId");

            migrationBuilder.AddForeignKey(
                name: "FK_CinemaMovies_Cinemas_CinemaId",
                table: "CinemaMovies",
                column: "CinemaId",
                principalTable: "Cinemas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CinemaMovies_Cinemas_CinemaId",
                table: "CinemaMovies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CinemaMovies",
                table: "CinemaMovies");

            migrationBuilder.DropIndex(
                name: "IX_CinemaMovies_CinemaId",
                table: "CinemaMovies");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CinemaMovies",
                newName: "CinemaId1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CinemaMovies",
                table: "CinemaMovies",
                column: "CinemaId");

            migrationBuilder.CreateIndex(
                name: "IX_CinemaMovies_CinemaId1",
                table: "CinemaMovies",
                column: "CinemaId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CinemaMovies_Cinemas_CinemaId1",
                table: "CinemaMovies",
                column: "CinemaId1",
                principalTable: "Cinemas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
