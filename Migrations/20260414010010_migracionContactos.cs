using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sigevet.Migrations
{
    /// <inheritdoc />
    public partial class migracionContactos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "fechaActualizacion",
                table: "TiposIdentificacion",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "fechaCreacion",
                table: "TiposIdentificacion",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "fechaActualizacion",
                table: "Personas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "fechaCreacion",
                table: "Personas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "fechaActualizacion",
                table: "Estados",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "fechaCreacion",
                table: "Estados",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "TiposContacto",
                columns: table => new
                {
                    idTipoContacto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tipoContacto = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposContacto", x => x.idTipoContacto);
                });

            migrationBuilder.CreateTable(
                name: "Contactos",
                columns: table => new
                {
                    idContacto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    detalleContacto = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    idPersonaContacto = table.Column<int>(type: "int", nullable: false),
                    idTipoContacto = table.Column<int>(type: "int", nullable: false),
                    idEstadoContacto = table.Column<int>(type: "int", nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contactos", x => x.idContacto);
                    table.ForeignKey(
                        name: "FK_Contactos_Estados_idEstadoContacto",
                        column: x => x.idEstadoContacto,
                        principalTable: "Estados",
                        principalColumn: "idEstado",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contactos_Personas_idPersonaContacto",
                        column: x => x.idPersonaContacto,
                        principalTable: "Personas",
                        principalColumn: "idPersona");
                    table.ForeignKey(
                        name: "FK_Contactos_TiposContacto_idTipoContacto",
                        column: x => x.idTipoContacto,
                        principalTable: "TiposContacto",
                        principalColumn: "idTipoContacto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contactos_idEstadoContacto",
                table: "Contactos",
                column: "idEstadoContacto");

            migrationBuilder.CreateIndex(
                name: "IX_Contactos_idPersonaContacto",
                table: "Contactos",
                column: "idPersonaContacto");

            migrationBuilder.CreateIndex(
                name: "IX_Contactos_idTipoContacto",
                table: "Contactos",
                column: "idTipoContacto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contactos");

            migrationBuilder.DropTable(
                name: "TiposContacto");

            migrationBuilder.DropColumn(
                name: "fechaActualizacion",
                table: "TiposIdentificacion");

            migrationBuilder.DropColumn(
                name: "fechaCreacion",
                table: "TiposIdentificacion");

            migrationBuilder.DropColumn(
                name: "fechaActualizacion",
                table: "Personas");

            migrationBuilder.DropColumn(
                name: "fechaCreacion",
                table: "Personas");

            migrationBuilder.DropColumn(
                name: "fechaActualizacion",
                table: "Estados");

            migrationBuilder.DropColumn(
                name: "fechaCreacion",
                table: "Estados");
        }
    }
}
