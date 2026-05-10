using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sigevet.Migrations
{
    /// <inheritdoc />
    public partial class FieldForSoftDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Vacunas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Vacunaciones",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "UnidadesMedida",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "TutoresMascota",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "TokensCuentas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "TiposVacuna",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "TiposMovimiento",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "TiposInsumo",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "TiposIdentificacion",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "TiposContacto",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "TiposAlerta",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "RolesParticipacion",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Razas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Personas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "MovimientoInventario",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Mascotas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Laboratorios",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Inventarios",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "InsumosSanitarios",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Estados",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "EsquemasVacunacion",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Especies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "EspecialidadesVeterinario",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Especialidades",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "CuentasUsuarios",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Contactos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "CategoriasEstado",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "BrigadasVeterinario",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Brigadas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "AlertasVacunacion",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Vacunas");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Vacunaciones");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "UnidadesMedida");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "TutoresMascota");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "TokensCuentas");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "TiposVacuna");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "TiposMovimiento");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "TiposInsumo");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "TiposIdentificacion");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "TiposContacto");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "TiposAlerta");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "RolesParticipacion");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Razas");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Personas");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "MovimientoInventario");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Mascotas");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Laboratorios");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Inventarios");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "InsumosSanitarios");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Estados");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "EsquemasVacunacion");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Especies");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "EspecialidadesVeterinario");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Especialidades");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "CuentasUsuarios");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Contactos");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "CategoriasEstado");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "BrigadasVeterinario");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Brigadas");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "AlertasVacunacion");
        }
    }
}
