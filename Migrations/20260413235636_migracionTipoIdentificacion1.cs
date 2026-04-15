using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sigevet.Migrations
{
    /// <inheritdoc />
    public partial class migracionTipoIdentificacion1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personas_TiposIdentificacion_idTipoIdentificacion1",
                table: "Personas");

            migrationBuilder.RenameColumn(
                name: "idTipoIdentificacion1",
                table: "Personas",
                newName: "idTipoIidentificacion");

            migrationBuilder.RenameIndex(
                name: "IX_Personas_idTipoIdentificacion1",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personas_TiposIdentificacion_idTipoIidentificacion",
                table: "Personas");

            migrationBuilder.RenameColumn(
                name: "idTipoIidentificacion",
                table: "Personas",
                newName: "idTipoIdentificacion1");

            migrationBuilder.RenameIndex(
                name: "IX_Personas_idTipoIidentificacion",
                table: "Personas",
                newName: "IX_Personas_idTipoIdentificacion1");

            migrationBuilder.AddForeignKey(
                name: "FK_Personas_TiposIdentificacion_idTipoIdentificacion1",
                table: "Personas",
                column: "idTipoIdentificacion1",
                principalTable: "TiposIdentificacion",
                principalColumn: "idTipoIdentificacion",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
