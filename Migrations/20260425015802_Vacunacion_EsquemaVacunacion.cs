using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sigevet.Migrations
{
    /// <inheritdoc />
    public partial class Vacunacion_EsquemaVacunacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EsquemasVacunacion",
                columns: table => new
                {
                    idEsquemaVacunacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    esquemaVacunacion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    intervaloDias = table.Column<int>(type: "int", nullable: false),
                    edadMinimaDias = table.Column<int>(type: "int", nullable: true),
                    observaciones = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    idTipoVacuna = table.Column<int>(type: "int", nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EsquemasVacunacion", x => x.idEsquemaVacunacion);
                    table.ForeignKey(
                        name: "FK_EsquemasVacunacion_TiposVacuna_idTipoVacuna",
                        column: x => x.idTipoVacuna,
                        principalTable: "TiposVacuna",
                        principalColumn: "idTipoVacuna",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vacunaciones",
                columns: table => new
                {
                    idVacunacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fechaAplicacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dosisAplicada = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    numeroDosis = table.Column<int>(type: "int", nullable: false),
                    observaciones = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    proximaFecha = table.Column<DateOnly>(type: "date", nullable: true),
                    idVacuna = table.Column<int>(type: "int", nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacunaciones", x => x.idVacunacion);
                    table.ForeignKey(
                        name: "FK_Vacunaciones_Vacunas_idVacuna",
                        column: x => x.idVacuna,
                        principalTable: "Vacunas",
                        principalColumn: "idVacuna",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EsquemasVacunacion_idTipoVacuna",
                table: "EsquemasVacunacion",
                column: "idTipoVacuna");

            migrationBuilder.CreateIndex(
                name: "IX_Vacunaciones_idVacuna",
                table: "Vacunaciones",
                column: "idVacuna");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EsquemasVacunacion");

            migrationBuilder.DropTable(
                name: "Vacunaciones");
        }
    }
}
