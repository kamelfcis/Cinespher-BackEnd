using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEnd.Migrations
{
    public partial class init4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CinemaHalls",
                columns: table => new
                {
                    CinemaHallId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Capacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CinemaHalls", x => x.CinemaHallId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Seats_CinemaHallId",
                table: "Seats",
                column: "CinemaHallId");

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_CinemaHalls_CinemaHallId",
                table: "Seats",
                column: "CinemaHallId",
                principalTable: "CinemaHalls",
                principalColumn: "CinemaHallId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seats_CinemaHalls_CinemaHallId",
                table: "Seats");

            migrationBuilder.DropTable(
                name: "CinemaHalls");

            migrationBuilder.DropIndex(
                name: "IX_Seats_CinemaHallId",
                table: "Seats");
        }
    }
}
