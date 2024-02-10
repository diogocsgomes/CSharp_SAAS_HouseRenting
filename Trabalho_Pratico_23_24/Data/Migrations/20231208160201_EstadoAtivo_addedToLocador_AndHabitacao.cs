using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trabalho_Pratico_23_24.Data.Migrations
{
    public partial class EstadoAtivo_addedToLocador_AndHabitacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Estado",
                table: "Habitacao",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Habitacao");
        }
    }
}
