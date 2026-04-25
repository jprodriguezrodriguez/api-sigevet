using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sigevet.Migrations
{
    /// <inheritdoc />
    public partial class Vacuna_Laboratorio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "idLabotarioContacto",
                table: "Contactos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Laboratorios",
                columns: table => new
                {
                    idLaboratorio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    laboratorio = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    direccion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Laboratorios", x => x.idLaboratorio);
                });

            migrationBuilder.CreateTable(
                name: "TiposVacuna",
                columns: table => new
                {
                    idTipoVacuna = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tipoVacuna = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposVacuna", x => x.idTipoVacuna);
                });

            migrationBuilder.CreateTable(
                name: "Vacunas",
                columns: table => new
                {
                    idVacuna = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    numeroLote = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    fechaFabricacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaVencimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    idTipoVacuna = table.Column<int>(type: "int", nullable: false),
                    idLaboratorio = table.Column<int>(type: "int", nullable: false),
                    idEstadoVacuna = table.Column<int>(type: "int", nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacunas", x => x.idVacuna);
                    table.ForeignKey(
                        name: "FK_Vacunas_Estados_idEstadoVacuna",
                        column: x => x.idEstadoVacuna,
                        principalTable: "Estados",
                        principalColumn: "idEstado");
                    table.ForeignKey(
                        name: "FK_Vacunas_Laboratorios_idLaboratorio",
                        column: x => x.idLaboratorio,
                        principalTable: "Laboratorios",
                        principalColumn: "idLaboratorio");
                    table.ForeignKey(
                        name: "FK_Vacunas_TiposVacuna_idTipoVacuna",
                        column: x => x.idTipoVacuna,
                        principalTable: "TiposVacuna",
                        principalColumn: "idTipoVacuna");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contactos_idLabotarioContacto",
                table: "Contactos",
                column: "idLabotarioContacto");

            migrationBuilder.CreateIndex(
                name: "IX_Vacunas_idEstadoVacuna",
                table: "Vacunas",
                column: "idEstadoVacuna");

            migrationBuilder.CreateIndex(
                name: "IX_Vacunas_idLaboratorio",
                table: "Vacunas",
                column: "idLaboratorio");

            migrationBuilder.CreateIndex(
                name: "IX_Vacunas_idTipoVacuna",
                table: "Vacunas",
                column: "idTipoVacuna");

            migrationBuilder.AddForeignKey(
                name: "FK_Contactos_Laboratorios_idLabotarioContacto",
                table: "Contactos",
                column: "idLabotarioContacto",
                principalTable: "Laboratorios",
                principalColumn: "idLaboratorio",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contactos_Laboratorios_idLabotarioContacto",
                table: "Contactos");

            migrationBuilder.DropTable(
                name: "Vacunas");

            migrationBuilder.DropTable(
                name: "Laboratorios");

            migrationBuilder.DropTable(
                name: "TiposVacuna");

            migrationBuilder.DropIndex(
                name: "IX_Contactos_idLabotarioContacto",
                table: "Contactos");

            migrationBuilder.DropColumn(
                name: "idLabotarioContacto",
                table: "Contactos");
        }
    }
}
