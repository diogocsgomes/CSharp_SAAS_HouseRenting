using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trabalho_Pratico_23_24.Data.Migrations
{
    public partial class FKAddedToArrendamento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Arrendamentos_Habitacao_ImovelId",
                table: "Arrendamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_Arrendamentos_Locadores_LocadorId",
                table: "Arrendamentos");

            migrationBuilder.RenameColumn(
                name: "ImovelId",
                table: "Arrendamentos",
                newName: "LocadorId1");

            migrationBuilder.RenameIndex(
                name: "IX_Arrendamentos_ImovelId",
                table: "Arrendamentos",
                newName: "IX_Arrendamentos_LocadorId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Arrendamentos_Habitacao_LocadorId",
                table: "Arrendamentos",
                column: "LocadorId",
                principalTable: "Habitacao",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Arrendamentos_Locadores_LocadorId1",
                table: "Arrendamentos",
                column: "LocadorId1",
                principalTable: "Locadores",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Arrendamentos_Habitacao_LocadorId",
                table: "Arrendamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_Arrendamentos_Locadores_LocadorId1",
                table: "Arrendamentos");

            migrationBuilder.RenameColumn(
                name: "LocadorId1",
                table: "Arrendamentos",
                newName: "ImovelId");

            migrationBuilder.RenameIndex(
                name: "IX_Arrendamentos_LocadorId1",
                table: "Arrendamentos",
                newName: "IX_Arrendamentos_ImovelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Arrendamentos_Habitacao_ImovelId",
                table: "Arrendamentos",
                column: "ImovelId",
                principalTable: "Habitacao",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Arrendamentos_Locadores_LocadorId",
                table: "Arrendamentos",
                column: "LocadorId",
                principalTable: "Locadores",
                principalColumn: "Id");
        }
    }
}
