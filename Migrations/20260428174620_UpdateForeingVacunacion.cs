using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sigevet.Migrations
{
    /// <inheritdoc />
    public partial class UpdateForeingVacunacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vacunaciones_Vacunas_idVacuna",
                table: "Vacunaciones");

            migrationBuilder.RenameColumn(
                name: "idVacuna",
                table: "Vacunaciones",
                newName: "idEsquemaVacunacion");

            migrationBuilder.RenameIndex(
                name: "IX_Vacunaciones_idVacuna",
                table: "Vacunaciones",
                newName: "IX_Vacunaciones_idEsquemaVacunacion");

            migrationBuilder.AddForeignKey(
                name: "FK_Vacunaciones_EsquemasVacunacion_idEsquemaVacunacion",
                table: "Vacunaciones",
                column: "idEsquemaVacunacion",
                principalTable: "EsquemasVacunacion",
                principalColumn: "idEsquemaVacunacion",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vacunaciones_EsquemasVacunacion_idEsquemaVacunacion",
                table: "Vacunaciones");

            migrationBuilder.RenameColumn(
                name: "idEsquemaVacunacion",
                table: "Vacunaciones",
                newName: "idVacuna");

            migrationBuilder.RenameIndex(
                name: "IX_Vacunaciones_idEsquemaVacunacion",
                table: "Vacunaciones",
                newName: "IX_Vacunaciones_idVacuna");

            migrationBuilder.AddForeignKey(
                name: "FK_Vacunaciones_Vacunas_idVacuna",
                table: "Vacunaciones",
                column: "idVacuna",
                principalTable: "Vacunas",
                principalColumn: "idVacuna",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
