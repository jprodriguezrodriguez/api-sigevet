using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sigevet.Migrations
{
    /// <inheritdoc />
    public partial class TablasIntermediasMascotasRazasEspecies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Veterinarios_Especialidad_EspecialidadidEspecialidad",
                table: "Veterinarios");

            migrationBuilder.DropIndex(
                name: "IX_Veterinarios_EspecialidadidEspecialidad",
                table: "Veterinarios");

            migrationBuilder.DropColumn(
                name: "EspecialidadidEspecialidad",
                table: "Veterinarios");

            migrationBuilder.CreateTable(
                name: "EspecialidadesVeterinario",
                columns: table => new
                {
                    idVeterinarioEspecialidad = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idVeterinario = table.Column<int>(type: "int", nullable: false),
                    idEspecialidad = table.Column<int>(type: "int", nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EspecialidadesVeterinario", x => x.idVeterinarioEspecialidad);
                    table.ForeignKey(
                        name: "FK_EspecialidadesVeterinario_Especialidad_idEspecialidad",
                        column: x => x.idEspecialidad,
                        principalTable: "Especialidad",
                        principalColumn: "idEspecialidad",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EspecialidadesVeterinario_Veterinarios_idVeterinario",
                        column: x => x.idVeterinario,
                        principalTable: "Veterinarios",
                        principalColumn: "idPersonaVet",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Especie",
                columns: table => new
                {
                    idEspecie = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    especie = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Especie", x => x.idEspecie);
                });

            migrationBuilder.CreateTable(
                name: "Razas",
                columns: table => new
                {
                    idRaza = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    raza = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    idEspecie = table.Column<int>(type: "int", nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Razas", x => x.idRaza);
                    table.ForeignKey(
                        name: "FK_Razas_Especie_idEspecie",
                        column: x => x.idEspecie,
                        principalTable: "Especie",
                        principalColumn: "idEspecie",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mascotas",
                columns: table => new
                {
                    idMascota = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    fechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    sexo = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    color = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    peso = table.Column<double>(type: "float(10)", precision: 10, scale: 2, nullable: false),
                    seniasParticulares = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    idRaza = table.Column<int>(type: "int", nullable: false),
                    idEstadoMascota = table.Column<int>(type: "int", nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mascotas", x => x.idMascota);
                    table.ForeignKey(
                        name: "FK_Mascotas_Estados_idEstadoMascota",
                        column: x => x.idEstadoMascota,
                        principalTable: "Estados",
                        principalColumn: "idEstado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mascotas_Razas_idRaza",
                        column: x => x.idRaza,
                        principalTable: "Razas",
                        principalColumn: "idRaza",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TutoresMascota",
                columns: table => new
                {
                    idTutorMascota = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idPersonaTut = table.Column<int>(type: "int", nullable: false),
                    idMascota = table.Column<int>(type: "int", nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TutoresMascota", x => x.idTutorMascota);
                    table.ForeignKey(
                        name: "FK_TutoresMascota_Mascotas_idMascota",
                        column: x => x.idMascota,
                        principalTable: "Mascotas",
                        principalColumn: "idMascota",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TutoresMascota_Tutores_idPersonaTut",
                        column: x => x.idPersonaTut,
                        principalTable: "Tutores",
                        principalColumn: "idPersonaTut",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EspecialidadesVeterinario_idEspecialidad",
                table: "EspecialidadesVeterinario",
                column: "idEspecialidad");

            migrationBuilder.CreateIndex(
                name: "IX_EspecialidadesVeterinario_idVeterinario",
                table: "EspecialidadesVeterinario",
                column: "idVeterinario");

            migrationBuilder.CreateIndex(
                name: "IX_Mascotas_idEstadoMascota",
                table: "Mascotas",
                column: "idEstadoMascota");

            migrationBuilder.CreateIndex(
                name: "IX_Mascotas_idRaza",
                table: "Mascotas",
                column: "idRaza");

            migrationBuilder.CreateIndex(
                name: "IX_Razas_idEspecie",
                table: "Razas",
                column: "idEspecie");

            migrationBuilder.CreateIndex(
                name: "IX_TutoresMascota_idMascota",
                table: "TutoresMascota",
                column: "idMascota");

            migrationBuilder.CreateIndex(
                name: "IX_TutoresMascota_idPersonaTut",
                table: "TutoresMascota",
                column: "idPersonaTut");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EspecialidadesVeterinario");

            migrationBuilder.DropTable(
                name: "TutoresMascota");

            migrationBuilder.DropTable(
                name: "Mascotas");

            migrationBuilder.DropTable(
                name: "Razas");

            migrationBuilder.DropTable(
                name: "Especie");

            migrationBuilder.AddColumn<int>(
                name: "EspecialidadidEspecialidad",
                table: "Veterinarios",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Veterinarios_EspecialidadidEspecialidad",
                table: "Veterinarios",
                column: "EspecialidadidEspecialidad");

            migrationBuilder.AddForeignKey(
                name: "FK_Veterinarios_Especialidad_EspecialidadidEspecialidad",
                table: "Veterinarios",
                column: "EspecialidadidEspecialidad",
                principalTable: "Especialidad",
                principalColumn: "idEspecialidad");
        }
    }
}
