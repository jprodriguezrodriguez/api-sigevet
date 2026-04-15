using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sigevet.Migrations
{
    /// <inheritdoc />
    public partial class migracionEstadosPorPersona : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personas_Estados_estadoPersonaidEstado",
                table: "Personas");

            migrationBuilder.RenameColumn(
                name: "estadoPersonaidEstado",
                table: "Personas",
                newName: "idEstadoPersona");

            migrationBuilder.RenameIndex(
                name: "IX_Personas_estadoPersonaidEstado",
                table: "Personas",
                newName: "IX_Personas_idEstadoPersona");

            migrationBuilder.AddForeignKey(
                name: "FK_Personas_Estados_idEstadoPersona",
                table: "Personas",
                column: "idEstadoPersona",
                principalTable: "Estados",
                principalColumn: "idEstado",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personas_Estados_idEstadoPersona",
                table: "Personas");

            migrationBuilder.RenameColumn(
                name: "idEstadoPersona",
                table: "Personas",
                newName: "estadoPersonaidEstado");

            migrationBuilder.RenameIndex(
                name: "IX_Personas_idEstadoPersona",
                table: "Personas",
                newName: "IX_Personas_estadoPersonaidEstado");

            migrationBuilder.AddForeignKey(
                name: "FK_Personas_Estados_estadoPersonaidEstado",
                table: "Personas",
                column: "estadoPersonaidEstado",
                principalTable: "Estados",
                principalColumn: "idEstado",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
