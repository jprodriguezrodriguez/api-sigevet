using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sigevet.Migrations
{
    /// <inheritdoc />
    public partial class migracionTipoIdentificacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "estadoPersonaidEstado",
                table: "Personas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "idTipoIdentificacion1",
                table: "Personas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Estados",
                columns: table => new
                {
                    idEstado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    estado = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estados", x => x.idEstado);
                });

            migrationBuilder.CreateTable(
                name: "TiposIdentificacion",
                columns: table => new
                {
                    idTipoIdentificacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tipoIdentificacion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposIdentificacion", x => x.idTipoIdentificacion);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Personas_estadoPersonaidEstado",
                table: "Personas",
                column: "estadoPersonaidEstado");

            migrationBuilder.CreateIndex(
                name: "IX_Personas_idTipoIdentificacion1",
                table: "Personas",
                column: "idTipoIdentificacion1");

            migrationBuilder.AddForeignKey(
                name: "FK_Personas_Estados_estadoPersonaidEstado",
                table: "Personas",
                column: "estadoPersonaidEstado",
                principalTable: "Estados",
                principalColumn: "idEstado",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Personas_TiposIdentificacion_idTipoIdentificacion1",
                table: "Personas",
                column: "idTipoIdentificacion1",
                principalTable: "TiposIdentificacion",
                principalColumn: "idTipoIdentificacion",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personas_Estados_estadoPersonaidEstado",
                table: "Personas");

            migrationBuilder.DropForeignKey(
                name: "FK_Personas_TiposIdentificacion_idTipoIdentificacion1",
                table: "Personas");

            migrationBuilder.DropTable(
                name: "Estados");

            migrationBuilder.DropTable(
                name: "TiposIdentificacion");

            migrationBuilder.DropIndex(
                name: "IX_Personas_estadoPersonaidEstado",
                table: "Personas");

            migrationBuilder.DropIndex(
                name: "IX_Personas_idTipoIdentificacion1",
                table: "Personas");

            migrationBuilder.DropColumn(
                name: "estadoPersonaidEstado",
                table: "Personas");

            migrationBuilder.DropColumn(
                name: "idTipoIdentificacion1",
                table: "Personas");
        }
    }
}
