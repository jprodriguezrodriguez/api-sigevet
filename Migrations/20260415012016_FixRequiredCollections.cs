using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sigevet.Migrations
{
    /// <inheritdoc />
    public partial class FixRequiredCollections : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personas_TiposIdentificacion_idTipoIidentificacion",
                table: "Personas");

            migrationBuilder.RenameColumn(
                name: "idTipoIidentificacion",
                table: "Personas",
                newName: "idTipoIdentificacion");

            migrationBuilder.RenameIndex(
                name: "IX_Personas_idTipoIidentificacion",
                table: "Personas",
                newName: "IX_Personas_idTipoIdentificacion");

            migrationBuilder.AddColumn<DateTime>(
                name: "fechaActualizacion",
                table: "TiposContacto",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "fechaCreacion",
                table: "TiposContacto",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Especialidad",
                columns: table => new
                {
                    idEspecialidad = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    especialidad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Especialidad", x => x.idEspecialidad);
                });

            migrationBuilder.CreateTable(
                name: "Veterinarios",
                columns: table => new
                {
                    idPersonaVet = table.Column<int>(type: "int", nullable: false),
                    numeroTarjetaProfesional = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    fechaRegistroVeterinario = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaActualizacionVeterinario = table.Column<DateTime>(type: "datetime2", nullable: false),
                    idEstadoDisponibilidad = table.Column<int>(type: "int", nullable: false),
                    estadoVeterinarioidEstado = table.Column<int>(type: "int", nullable: false),
                    EspecialidadidEspecialidad = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veterinarios", x => x.idPersonaVet);
                    table.ForeignKey(
                        name: "FK_Veterinarios_Especialidad_EspecialidadidEspecialidad",
                        column: x => x.EspecialidadidEspecialidad,
                        principalTable: "Especialidad",
                        principalColumn: "idEspecialidad");
                    table.ForeignKey(
                        name: "FK_Veterinarios_Estados_estadoVeterinarioidEstado",
                        column: x => x.estadoVeterinarioidEstado,
                        principalTable: "Estados",
                        principalColumn: "idEstado",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Veterinarios_Personas_idPersonaVet",
                        column: x => x.idPersonaVet,
                        principalTable: "Personas",
                        principalColumn: "idPersona");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Veterinarios_EspecialidadidEspecialidad",
                table: "Veterinarios",
                column: "EspecialidadidEspecialidad");

            migrationBuilder.CreateIndex(
                name: "IX_Veterinarios_estadoVeterinarioidEstado",
                table: "Veterinarios",
                column: "estadoVeterinarioidEstado");

            migrationBuilder.AddForeignKey(
                name: "FK_Personas_TiposIdentificacion_idTipoIdentificacion",
                table: "Personas",
                column: "idTipoIdentificacion",
                principalTable: "TiposIdentificacion",
                principalColumn: "idTipoIdentificacion",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personas_TiposIdentificacion_idTipoIdentificacion",
                table: "Personas");

            migrationBuilder.DropTable(
                name: "Veterinarios");

            migrationBuilder.DropTable(
                name: "Especialidad");

            migrationBuilder.DropColumn(
                name: "fechaActualizacion",
                table: "TiposContacto");

            migrationBuilder.DropColumn(
                name: "fechaCreacion",
                table: "TiposContacto");

            migrationBuilder.RenameColumn(
                name: "idTipoIdentificacion",
                table: "Personas",
                newName: "idTipoIidentificacion");

            migrationBuilder.RenameIndex(
                name: "IX_Personas_idTipoIdentificacion",
                table: "Personas",
                newName: "IX_Personas_idTipoIidentificacion");

            migrationBuilder.AddForeignKey(
                name: "FK_Personas_TiposIdentificacion_idTipoIidentificacion",
                table: "Personas",
                column: "idTipoIidentificacion",
                principalTable: "TiposIdentificacion",
                principalColumn: "idTipoIdentificacion",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
