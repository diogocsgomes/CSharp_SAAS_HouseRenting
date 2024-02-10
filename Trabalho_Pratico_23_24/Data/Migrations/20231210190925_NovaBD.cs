using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trabalho_Pratico_23_24.Data.Migrations
{
    public partial class NovaBD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CheckIns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoraEntrada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TemEquipamentosOpcionais = table.Column<bool>(type: "bit", nullable: false),
                    EquipamentoOpcionais = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TemDanos = table.Column<bool>(type: "bit", nullable: false),
                    Danos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observacoes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntregadorId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckIns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckIns_AspNetUsers_EntregadorId",
                        column: x => x.EntregadorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CheckOuts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoraSaida = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TemEquipamentosOpcionais = table.Column<bool>(type: "bit", nullable: false),
                    EquipamentoOpcionais = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TemDanos = table.Column<bool>(type: "bit", nullable: false),
                    Danos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observacoes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecetorId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckOuts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckOuts_AspNetUsers_RecetorId",
                        column: x => x.RecetorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Arrendamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ImovelId = table.Column<int>(type: "int", nullable: true),
                    DatraEntrada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatraSaida = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LocadorId = table.Column<int>(type: "int", nullable: true),
                    PorConfirmar = table.Column<bool>(type: "bit", nullable: false),
                    Aceite = table.Column<bool>(type: "bit", nullable: false),
                    EntreguePorCliente = table.Column<bool>(type: "bit", nullable: false),
                    RecebidoPeloLocador = table.Column<bool>(type: "bit", nullable: false),
                    CheckInId = table.Column<int>(type: "int", nullable: true),
                    CheckOutId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arrendamentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Arrendamentos_AspNetUsers_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Arrendamentos_CheckIns_CheckInId",
                        column: x => x.CheckInId,
                        principalTable: "CheckIns",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Arrendamentos_CheckOuts_CheckOutId",
                        column: x => x.CheckOutId,
                        principalTable: "CheckOuts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Arrendamentos_Habitacao_ImovelId",
                        column: x => x.ImovelId,
                        principalTable: "Habitacao",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Arrendamentos_Locadores_LocadorId",
                        column: x => x.LocadorId,
                        principalTable: "Locadores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Arrendamentos_CheckInId",
                table: "Arrendamentos",
                column: "CheckInId");

            migrationBuilder.CreateIndex(
                name: "IX_Arrendamentos_CheckOutId",
                table: "Arrendamentos",
                column: "CheckOutId");

            migrationBuilder.CreateIndex(
                name: "IX_Arrendamentos_ClienteId",
                table: "Arrendamentos",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Arrendamentos_ImovelId",
                table: "Arrendamentos",
                column: "ImovelId");

            migrationBuilder.CreateIndex(
                name: "IX_Arrendamentos_LocadorId",
                table: "Arrendamentos",
                column: "LocadorId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckIns_EntregadorId",
                table: "CheckIns",
                column: "EntregadorId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckOuts_RecetorId",
                table: "CheckOuts",
                column: "RecetorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Arrendamentos");

            migrationBuilder.DropTable(
                name: "CheckIns");

            migrationBuilder.DropTable(
                name: "CheckOuts");
        }
    }
}
