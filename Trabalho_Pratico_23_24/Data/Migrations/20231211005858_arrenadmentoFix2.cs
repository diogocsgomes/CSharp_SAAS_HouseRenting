using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trabalho_Pratico_23_24.Data.Migrations
{
    public partial class arrenadmentoFix2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Arrendamentos_Habitacao_LocadorId",
                table: "Arrendamentos");

            migrationBuilder.AlterColumn<int>(
                name: "LocadorId",
                table: "Arrendamentos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Arrendamentos_Habitacao_LocadorId",
                table: "Arrendamentos",
                column: "LocadorId",
                principalTable: "Habitacao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Arrendamentos_Habitacao_LocadorId",
                table: "Arrendamentos");

            migrationBuilder.AlterColumn<int>(
                name: "LocadorId",
                table: "Arrendamentos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Arrendamentos_Habitacao_LocadorId",
                table: "Arrendamentos",
                column: "LocadorId",
                principalTable: "Habitacao",
                principalColumn: "Id");
        }
    }
}
