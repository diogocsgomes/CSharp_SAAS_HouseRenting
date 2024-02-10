using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trabalho_Pratico_23_24.Data.Migrations
{
    public partial class AppUserReadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UtilizadorId",
                table: "Funcionarios",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Funcionarios_UtilizadorId",
                table: "Funcionarios",
                column: "UtilizadorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Funcionarios_AspNetUsers_UtilizadorId",
                table: "Funcionarios",
                column: "UtilizadorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Funcionarios_AspNetUsers_UtilizadorId",
                table: "Funcionarios");

            migrationBuilder.DropIndex(
                name: "IX_Funcionarios_UtilizadorId",
                table: "Funcionarios");

            migrationBuilder.DropColumn(
                name: "UtilizadorId",
                table: "Funcionarios");
        }
    }
}
