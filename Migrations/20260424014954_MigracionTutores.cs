using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sigevet.Migrations
{
    /// <inheritdoc />
    public partial class MigracionTutores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Veterinarios_Estados_estadoVeterinarioidEstado",
                table: "Veterinarios");

            migrationBuilder.DropIndex(
                name: "IX_Veterinarios_estadoVeterinarioidEstado",
                table: "Veterinarios");

            migrationBuilder.DropColumn(
                name: "estadoVeterinarioidEstado",
                table: "Veterinarios");

            migrationBuilder.CreateTable(
                name: "Tutores",
                columns: table => new
                {
                    idPersonaTut = table.Column<int>(type: "int", nullable: false),
                    autorizaNotificaciones = table.Column<bool>(type: "bit", nullable: false),
                    fechaRegistroTutor = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaActualizacionTutor = table.Column<DateTime>(type: "datetime2", nullable: false),
                    idEstadoCuentaTutor = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tutores", x => x.idPersonaTut);
                    table.ForeignKey(
                        name: "FK_Tutores_Estados_idEstadoCuentaTutor",
                        column: x => x.idEstadoCuentaTutor,
                        principalTable: "Estados",
                        principalColumn: "idEstado");
                    table.ForeignKey(
                        name: "FK_Tutores_Personas_idPersonaTut",
                        column: x => x.idPersonaTut,
                        principalTable: "Personas",
                        principalColumn: "idPersona");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Veterinarios_idEstadoDisponibilidad",
                table: "Veterinarios",
                column: "idEstadoDisponibilidad");

            migrationBuilder.CreateIndex(
                name: "IX_Tutores_idEstadoCuentaTutor",
                table: "Tutores",
                column: "idEstadoCuentaTutor");

            migrationBuilder.AddForeignKey(
                name: "FK_Veterinarios_Estados_idEstadoDisponibilidad",
                table: "Veterinarios",
                column: "idEstadoDisponibilidad",
                principalTable: "Estados",
                principalColumn: "idEstado",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Veterinarios_Estados_idEstadoDisponibilidad",
                table: "Veterinarios");

            migrationBuilder.DropTable(
                name: "Tutores");

            migrationBuilder.DropIndex(
                name: "IX_Veterinarios_idEstadoDisponibilidad",
                table: "Veterinarios");

            migrationBuilder.AddColumn<int>(
                name: "estadoVeterinarioidEstado",
                table: "Veterinarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Veterinarios_estadoVeterinarioidEstado",
                table: "Veterinarios",
                column: "estadoVeterinarioidEstado");

            migrationBuilder.AddForeignKey(
                name: "FK_Veterinarios_Estados_estadoVeterinarioidEstado",
                table: "Veterinarios",
                column: "estadoVeterinarioidEstado",
                principalTable: "Estados",
                principalColumn: "idEstado",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
