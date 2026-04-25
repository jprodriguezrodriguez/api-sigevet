using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sigevet.Migrations
{
    /// <inheritdoc />
    public partial class TransctionalInventario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EspecialidadesVeterinario_Especialidad_idEspecialidad",
                table: "EspecialidadesVeterinario");

            migrationBuilder.DropForeignKey(
                name: "FK_Razas_Especie_idEspecie",
                table: "Razas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Especie",
                table: "Especie");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Especialidad",
                table: "Especialidad");

            migrationBuilder.RenameTable(
                name: "Especie",
                newName: "Especies");

            migrationBuilder.RenameTable(
                name: "Especialidad",
                newName: "Especialidades");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Especies",
                table: "Especies",
                column: "idEspecie");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Especialidades",
                table: "Especialidades",
                column: "idEspecialidad");

            migrationBuilder.CreateTable(
                name: "Brigadas",
                columns: table => new
                {
                    idBrigada = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombreBrigada = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    fechaBrigada = table.Column<DateOnly>(type: "date", nullable: false),
                    horaInicio = table.Column<TimeOnly>(type: "time", nullable: false),
                    horaFin = table.Column<TimeOnly>(type: "time", nullable: false),
                    ubicacion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    cobertura = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    observaciones = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    idEstadoBrigada = table.Column<int>(type: "int", nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brigadas", x => x.idBrigada);
                    table.ForeignKey(
                        name: "FK_Brigadas_Estados_idEstadoBrigada",
                        column: x => x.idEstadoBrigada,
                        principalTable: "Estados",
                        principalColumn: "idEstado");
                });

            migrationBuilder.CreateTable(
                name: "TiposInsumo",
                columns: table => new
                {
                    idTipoInsumo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tipoInsumo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposInsumo", x => x.idTipoInsumo);
                });

            migrationBuilder.CreateTable(
                name: "TiposMovimiento",
                columns: table => new
                {
                    idTipoMovimiento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tipoMovimiento = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposMovimiento", x => x.idTipoMovimiento);
                });

            migrationBuilder.CreateTable(
                name: "UnidadesMedida",
                columns: table => new
                {
                    idUnidadMedida = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    unidadMedida = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadesMedida", x => x.idUnidadMedida);
                });

            migrationBuilder.CreateTable(
                name: "InsumosSanitarios",
                columns: table => new
                {
                    idInsumoSanitario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    insumoSanitario = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    idTipoInsumo = table.Column<int>(type: "int", nullable: false),
                    idUnidadMedida = table.Column<int>(type: "int", nullable: false),
                    idEstadoInsumo = table.Column<int>(type: "int", nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsumosSanitarios", x => x.idInsumoSanitario);
                    table.ForeignKey(
                        name: "FK_InsumosSanitarios_Estados_idEstadoInsumo",
                        column: x => x.idEstadoInsumo,
                        principalTable: "Estados",
                        principalColumn: "idEstado");
                    table.ForeignKey(
                        name: "FK_InsumosSanitarios_TiposInsumo_idTipoInsumo",
                        column: x => x.idTipoInsumo,
                        principalTable: "TiposInsumo",
                        principalColumn: "idTipoInsumo");
                    table.ForeignKey(
                        name: "FK_InsumosSanitarios_UnidadesMedida_idUnidadMedida",
                        column: x => x.idUnidadMedida,
                        principalTable: "UnidadesMedida",
                        principalColumn: "idUnidadMedida");
                });

            migrationBuilder.CreateTable(
                name: "Inventarios",
                columns: table => new
                {
                    idInventario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cantidadDisponible = table.Column<int>(type: "int", nullable: false),
                    stockMinimo = table.Column<int>(type: "int", nullable: false),
                    idInsumoSanitario = table.Column<int>(type: "int", nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventarios", x => x.idInventario);
                    table.ForeignKey(
                        name: "FK_Inventarios_InsumosSanitarios_idInsumoSanitario",
                        column: x => x.idInsumoSanitario,
                        principalTable: "InsumosSanitarios",
                        principalColumn: "idInsumoSanitario");
                });

            migrationBuilder.CreateTable(
                name: "MovimientoInventario",
                columns: table => new
                {
                    idMovimientoInventario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cantidad = table.Column<int>(type: "int", nullable: false),
                    fechaMovimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    motivo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    observaciones = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    idTipoMovimiento = table.Column<int>(type: "int", nullable: false),
                    idResponsable = table.Column<int>(type: "int", nullable: false),
                    idInventario = table.Column<int>(type: "int", nullable: false),
                    idBrigada = table.Column<int>(type: "int", nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimientoInventario", x => x.idMovimientoInventario);
                    table.ForeignKey(
                        name: "FK_MovimientoInventario_Brigadas_idBrigada",
                        column: x => x.idBrigada,
                        principalTable: "Brigadas",
                        principalColumn: "idBrigada");
                    table.ForeignKey(
                        name: "FK_MovimientoInventario_Inventarios_idInventario",
                        column: x => x.idInventario,
                        principalTable: "Inventarios",
                        principalColumn: "idInventario");
                    table.ForeignKey(
                        name: "FK_MovimientoInventario_Personas_idResponsable",
                        column: x => x.idResponsable,
                        principalTable: "Personas",
                        principalColumn: "idPersona");
                    table.ForeignKey(
                        name: "FK_MovimientoInventario_TiposMovimiento_idTipoMovimiento",
                        column: x => x.idTipoMovimiento,
                        principalTable: "TiposMovimiento",
                        principalColumn: "idTipoMovimiento");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Brigadas_idEstadoBrigada",
                table: "Brigadas",
                column: "idEstadoBrigada");

            migrationBuilder.CreateIndex(
                name: "IX_InsumosSanitarios_idEstadoInsumo",
                table: "InsumosSanitarios",
                column: "idEstadoInsumo");

            migrationBuilder.CreateIndex(
                name: "IX_InsumosSanitarios_idTipoInsumo",
                table: "InsumosSanitarios",
                column: "idTipoInsumo");

            migrationBuilder.CreateIndex(
                name: "IX_InsumosSanitarios_idUnidadMedida",
                table: "InsumosSanitarios",
                column: "idUnidadMedida");

            migrationBuilder.CreateIndex(
                name: "IX_Inventarios_idInsumoSanitario",
                table: "Inventarios",
                column: "idInsumoSanitario");

            migrationBuilder.CreateIndex(
                name: "IX_MovimientoInventario_idBrigada",
                table: "MovimientoInventario",
                column: "idBrigada");

            migrationBuilder.CreateIndex(
                name: "IX_MovimientoInventario_idInventario",
                table: "MovimientoInventario",
                column: "idInventario");

            migrationBuilder.CreateIndex(
                name: "IX_MovimientoInventario_idResponsable",
                table: "MovimientoInventario",
                column: "idResponsable");

            migrationBuilder.CreateIndex(
                name: "IX_MovimientoInventario_idTipoMovimiento",
                table: "MovimientoInventario",
                column: "idTipoMovimiento");

            migrationBuilder.AddForeignKey(
                name: "FK_EspecialidadesVeterinario_Especialidades_idEspecialidad",
                table: "EspecialidadesVeterinario",
                column: "idEspecialidad",
                principalTable: "Especialidades",
                principalColumn: "idEspecialidad",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Razas_Especies_idEspecie",
                table: "Razas",
                column: "idEspecie",
                principalTable: "Especies",
                principalColumn: "idEspecie",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EspecialidadesVeterinario_Especialidades_idEspecialidad",
                table: "EspecialidadesVeterinario");

            migrationBuilder.DropForeignKey(
                name: "FK_Razas_Especies_idEspecie",
                table: "Razas");

            migrationBuilder.DropTable(
                name: "MovimientoInventario");

            migrationBuilder.DropTable(
                name: "Brigadas");

            migrationBuilder.DropTable(
                name: "Inventarios");

            migrationBuilder.DropTable(
                name: "TiposMovimiento");

            migrationBuilder.DropTable(
                name: "InsumosSanitarios");

            migrationBuilder.DropTable(
                name: "TiposInsumo");

            migrationBuilder.DropTable(
                name: "UnidadesMedida");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Especies",
                table: "Especies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Especialidades",
                table: "Especialidades");

            migrationBuilder.RenameTable(
                name: "Especies",
                newName: "Especie");

            migrationBuilder.RenameTable(
                name: "Especialidades",
                newName: "Especialidad");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Especie",
                table: "Especie",
                column: "idEspecie");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Especialidad",
                table: "Especialidad",
                column: "idEspecialidad");

            migrationBuilder.AddForeignKey(
                name: "FK_EspecialidadesVeterinario_Especialidad_idEspecialidad",
                table: "EspecialidadesVeterinario",
                column: "idEspecialidad",
                principalTable: "Especialidad",
                principalColumn: "idEspecialidad",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Razas_Especie_idEspecie",
                table: "Razas",
                column: "idEspecie",
                principalTable: "Especie",
                principalColumn: "idEspecie",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
