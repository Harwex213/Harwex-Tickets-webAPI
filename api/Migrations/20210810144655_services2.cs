using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class services2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ServiceName",
                table: "SessionServices",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SessionServices_ServiceName",
                table: "SessionServices",
                column: "ServiceName");

            migrationBuilder.CreateIndex(
                name: "IX_SessionServices_SessionId",
                table: "SessionServices",
                column: "SessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_SessionServices_Services_ServiceName",
                table: "SessionServices",
                column: "ServiceName",
                principalTable: "Services",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionServices_Sessions_SessionId",
                table: "SessionServices",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SessionServices_Services_ServiceName",
                table: "SessionServices");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionServices_Sessions_SessionId",
                table: "SessionServices");

            migrationBuilder.DropIndex(
                name: "IX_SessionServices_ServiceName",
                table: "SessionServices");

            migrationBuilder.DropIndex(
                name: "IX_SessionServices_SessionId",
                table: "SessionServices");

            migrationBuilder.DropColumn(
                name: "ServiceName",
                table: "SessionServices");
        }
    }
}
