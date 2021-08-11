using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class city2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Cinemas_CityId",
                table: "Cinemas",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cinemas_Cities_CityId",
                table: "Cinemas",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cinemas_Cities_CityId",
                table: "Cinemas");

            migrationBuilder.DropIndex(
                name: "IX_Cinemas_CityId",
                table: "Cinemas");
        }
    }
}
