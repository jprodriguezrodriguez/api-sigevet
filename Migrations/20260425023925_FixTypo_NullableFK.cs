using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sigevet.Migrations
{
    /// <inheritdoc />
    public partial class FixTypo_NullableFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contactos_Laboratorios_idLabotarioContacto",
                table: "Contactos");

            migrationBuilder.DropIndex(
                name: "IX_TutoresMascota_idPersonaTut",
                table: "TutoresMascota");

            migrationBuilder.DropIndex(
                name: "IX_EspecialidadesVeterinario_idVeterinario",
                table: "EspecialidadesVeterinario");

            migrationBuilder.DropIndex(
                name: "IX_Contactos_idLabotarioContacto",
                table: "Contactos");

            migrationBuilder.DropIndex(
                name: "IX_BrigadasVeterinario_idVeterinario",
                table: "BrigadasVeterinario");

            migrationBuilder.DropColumn(
                name: "idLabotarioContacto",
                table: "Contactos");

            migrationBuilder.AlterColumn<decimal>(
                name: "peso",
                table: "Mascotas",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float(10)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.AlterColumn<int>(
                name: "idPersonaContacto",
                table: "Contactos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "idLaboratorioContacto",
                table: "Contactos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "fechaActualizacion",
                table: "AlertasVacunacion",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "fechaCreacion",
                table: "AlertasVacunacion",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_TutoresMascota_idPersonaTut_idMascota",
                table: "TutoresMascota",
                columns: new[] { "idPersonaTut", "idMascota" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EspecialidadesVeterinario_idVeterinario_idEspecialidad",
                table: "EspecialidadesVeterinario",
                columns: new[] { "idVeterinario", "idEspecialidad" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contactos_idLaboratorioContacto",
                table: "Contactos",
                column: "idLaboratorioContacto");

            migrationBuilder.CreateIndex(
                name: "IX_BrigadasVeterinario_idVeterinario_idBrigada_idRolParticipacion",
                table: "BrigadasVeterinario",
                columns: new[] { "idVeterinario", "idBrigada", "idRolParticipacion" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Contactos_Laboratorios_idLaboratorioContacto",
                table: "Contactos",
                column: "idLaboratorioContacto",
                principalTable: "Laboratorios",
                principalColumn: "idLaboratorio");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contactos_Laboratorios_idLaboratorioContacto",
                table: "Contactos");

            migrationBuilder.DropIndex(
                name: "IX_TutoresMascota_idPersonaTut_idMascota",
                table: "TutoresMascota");

            migrationBuilder.DropIndex(
                name: "IX_EspecialidadesVeterinario_idVeterinario_idEspecialidad",
                table: "EspecialidadesVeterinario");

            migrationBuilder.DropIndex(
                name: "IX_Contactos_idLaboratorioContacto",
                table: "Contactos");

            migrationBuilder.DropIndex(
                name: "IX_BrigadasVeterinario_idVeterinario_idBrigada_idRolParticipacion",
                table: "BrigadasVeterinario");

            migrationBuilder.DropColumn(
                name: "idLaboratorioContacto",
                table: "Contactos");

            migrationBuilder.DropColumn(
                name: "fechaActualizacion",
                table: "AlertasVacunacion");

            migrationBuilder.DropColumn(
                name: "fechaCreacion",
                table: "AlertasVacunacion");

            migrationBuilder.AlterColumn<double>(
                name: "peso",
                table: "Mascotas",
                type: "float(10)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldPrecision: 5,
                oldScale: 2);

            migrationBuilder.AlterColumn<int>(
                name: "idPersonaContacto",
                table: "Contactos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "idLabotarioContacto",
                table: "Contactos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TutoresMascota_idPersonaTut",
                table: "TutoresMascota",
                column: "idPersonaTut");

            migrationBuilder.CreateIndex(
                name: "IX_EspecialidadesVeterinario_idVeterinario",
                table: "EspecialidadesVeterinario",
                column: "idVeterinario");

            migrationBuilder.CreateIndex(
                name: "IX_Contactos_idLabotarioContacto",
                table: "Contactos",
                column: "idLabotarioContacto");

            migrationBuilder.CreateIndex(
                name: "IX_BrigadasVeterinario_idVeterinario",
                table: "BrigadasVeterinario",
                column: "idVeterinario");

            migrationBuilder.AddForeignKey(
                name: "FK_Contactos_Laboratorios_idLabotarioContacto",
                table: "Contactos",
                column: "idLabotarioContacto",
                principalTable: "Laboratorios",
                principalColumn: "idLaboratorio",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
