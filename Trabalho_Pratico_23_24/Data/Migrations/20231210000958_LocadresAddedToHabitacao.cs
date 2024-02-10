using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trabalho_Pratico_23_24.Data.Migrations
{
    public partial class LocadresAddedToHabitacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Habitacao_Locadores_LocadorId",
                table: "Habitacao");

            migrationBuilder.DropIndex(
                name: "IX_Habitacao_LocadorId",
                table: "Habitacao");

            migrationBuilder.DropColumn(
                name: "LocadorId",
                table: "Habitacao");

            migrationBuilder.CreateTable(
                name: "HabitacaoLocador",
                columns: table => new
                {
                    LocadorId = table.Column<int>(type: "int", nullable: false),
                    LocadoresId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HabitacaoLocador", x => new { x.LocadorId, x.LocadoresId });
                    table.ForeignKey(
                        name: "FK_HabitacaoLocador_Habitacao_LocadorId",
                        column: x => x.LocadorId,
                        principalTable: "Habitacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HabitacaoLocador_Locadores_LocadoresId",
                        column: x => x.LocadoresId,
                        principalTable: "Locadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HabitacaoLocador_LocadoresId",
                table: "HabitacaoLocador",
                column: "LocadoresId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HabitacaoLocador");

            migrationBuilder.AddColumn<int>(
                name: "LocadorId",
                table: "Habitacao",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Habitacao_LocadorId",
                table: "Habitacao",
                column: "LocadorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Habitacao_Locadores_LocadorId",
                table: "Habitacao",
                column: "LocadorId",
                principalTable: "Locadores",
                principalColumn: "Id");
        }
    }
}
