using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sigevet.Migrations
{
    /// <inheritdoc />
    public partial class AlertaVacunacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "idMascota",
                table: "Vacunaciones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TiposAlerta",
                columns: table => new
                {
                    idTipoAlerta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    alerta = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposAlerta", x => x.idTipoAlerta);
                });

            migrationBuilder.CreateTable(
                name: "AlertasVacunacion",
                columns: table => new
                {
                    idAlerta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fechaGeneracion = table.Column<DateOnly>(type: "date", nullable: false),
                    fechaProgramada = table.Column<DateOnly>(type: "date", nullable: false),
                    mensaje = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    idTipoAlerta = table.Column<int>(type: "int", nullable: false),
                    idVacunacion = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlertasVacunacion", x => x.idAlerta);
                    table.ForeignKey(
                        name: "FK_AlertasVacunacion_TiposAlerta_idTipoAlerta",
                        column: x => x.idTipoAlerta,
                        principalTable: "TiposAlerta",
                        principalColumn: "idTipoAlerta",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AlertasVacunacion_Vacunaciones_idVacunacion",
                        column: x => x.idVacunacion,
                        principalTable: "Vacunaciones",
                        principalColumn: "idVacunacion",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vacunaciones_idMascota",
                table: "Vacunaciones",
                column: "idMascota");

            migrationBuilder.CreateIndex(
                name: "IX_AlertasVacunacion_idTipoAlerta",
                table: "AlertasVacunacion",
                column: "idTipoAlerta");

            migrationBuilder.CreateIndex(
                name: "IX_AlertasVacunacion_idVacunacion",
                table: "AlertasVacunacion",
                column: "idVacunacion");

            migrationBuilder.AddForeignKey(
                name: "FK_Vacunaciones_Mascotas_idMascota",
                table: "Vacunaciones",
                column: "idMascota",
                principalTable: "Mascotas",
                principalColumn: "idMascota",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vacunaciones_Mascotas_idMascota",
                table: "Vacunaciones");

            migrationBuilder.DropTable(
                name: "AlertasVacunacion");

            migrationBuilder.DropTable(
                name: "TiposAlerta");

            migrationBuilder.DropIndex(
                name: "IX_Vacunaciones_idMascota",
                table: "Vacunaciones");

            migrationBuilder.DropColumn(
                name: "idMascota",
                table: "Vacunaciones");
        }
    }
}
