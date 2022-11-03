using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastucture.Migrations
{
    public partial class removeServices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_SessionServices_SessionServiceId",
                table: "Tickets");

            migrationBuilder.DropTable(
                name: "SessionServices");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_SessionServiceId",
                table: "Tickets");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                    table.UniqueConstraint("AK_Services_Name", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "SessionServices",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    ServiceName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SessionId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SessionServices_Services_ServiceName",
                        column: x => x.ServiceName,
                        principalTable: "Services",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SessionServices_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_SessionServiceId",
                table: "Tickets",
                column: "SessionServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionServices_ServiceName",
                table: "SessionServices",
                column: "ServiceName");

            migrationBuilder.CreateIndex(
                name: "IX_SessionServices_SessionId",
                table: "SessionServices",
                column: "SessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_SessionServices_SessionServiceId",
                table: "Tickets",
                column: "SessionServiceId",
                principalTable: "SessionServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
