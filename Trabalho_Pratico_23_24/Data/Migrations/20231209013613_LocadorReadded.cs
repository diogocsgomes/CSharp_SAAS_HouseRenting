using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trabalho_Pratico_23_24.Data.Migrations
{
    public partial class LocadorReadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocadorId",
                table: "Funcionarios",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Funcionarios_LocadorId",
                table: "Funcionarios",
                column: "LocadorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Funcionarios_Locadores_LocadorId",
                table: "Funcionarios",
                column: "LocadorId",
                principalTable: "Locadores",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Funcionarios_Locadores_LocadorId",
                table: "Funcionarios");

            migrationBuilder.DropIndex(
                name: "IX_Funcionarios_LocadorId",
                table: "Funcionarios");

            migrationBuilder.DropColumn(
                name: "LocadorId",
                table: "Funcionarios");
        }
    }
}
