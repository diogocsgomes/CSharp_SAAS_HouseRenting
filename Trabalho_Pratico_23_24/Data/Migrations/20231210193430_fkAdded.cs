using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trabalho_Pratico_23_24.Data.Migrations
{
    public partial class fkAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckIns_AspNetUsers_EntregadorId",
                table: "CheckIns");

            migrationBuilder.DropForeignKey(
                name: "FK_CheckOuts_AspNetUsers_RecetorId",
                table: "CheckOuts");

            migrationBuilder.AlterColumn<string>(
                name: "RecetorId",
                table: "CheckOuts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EntregadorId",
                table: "CheckIns",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CheckIns_AspNetUsers_EntregadorId",
                table: "CheckIns",
                column: "EntregadorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CheckOuts_AspNetUsers_RecetorId",
                table: "CheckOuts",
                column: "RecetorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckIns_AspNetUsers_EntregadorId",
                table: "CheckIns");

            migrationBuilder.DropForeignKey(
                name: "FK_CheckOuts_AspNetUsers_RecetorId",
                table: "CheckOuts");

            migrationBuilder.AlterColumn<string>(
                name: "RecetorId",
                table: "CheckOuts",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "EntregadorId",
                table: "CheckIns",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckIns_AspNetUsers_EntregadorId",
                table: "CheckIns",
                column: "EntregadorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckOuts_AspNetUsers_RecetorId",
                table: "CheckOuts",
                column: "RecetorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
