using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trabalho_Pratico_23_24.Data.Migrations
{
    public partial class TryToFIxEstado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Habitacao");

            migrationBuilder.AlterColumn<bool>(
                name: "EstadoAtivo",
                table: "Locadores",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<bool>(
                name: "EstadoAtivo",
                table: "Habitacao",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstadoAtivo",
                table: "Habitacao");

            migrationBuilder.AlterColumn<bool>(
                name: "EstadoAtivo",
                table: "Locadores",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Estado",
                table: "Habitacao",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
