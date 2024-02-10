using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trabalho_Pratico_23_24.Data.Migrations
{
    public partial class LocadorAdded2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocadorId",
                table: "Habitacao",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Locadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    N_Telefone = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locadores", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Habitacao_Locadores_LocadorId",
                table: "Habitacao");

            migrationBuilder.DropTable(
                name: "Locadores");

            migrationBuilder.DropIndex(
                name: "IX_Habitacao_LocadorId",
                table: "Habitacao");

            migrationBuilder.DropColumn(
                name: "LocadorId",
                table: "Habitacao");
        }
    }
}
