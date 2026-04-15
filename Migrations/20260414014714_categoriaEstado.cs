using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sigevet.Migrations
{
    /// <inheritdoc />
    public partial class categoriaEstado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "idCategoriaEstado",
                table: "Estados",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CategoriasEstado",
                columns: table => new
                {
                    idCategoriaEstado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    categoriaEstado = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriasEstado", x => x.idCategoriaEstado);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Estados_idCategoriaEstado",
                table: "Estados",
                column: "idCategoriaEstado");

            migrationBuilder.AddForeignKey(
                name: "FK_Estados_CategoriasEstado_idCategoriaEstado",
                table: "Estados",
                column: "idCategoriaEstado",
                principalTable: "CategoriasEstado",
                principalColumn: "idCategoriaEstado",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estados_CategoriasEstado_idCategoriaEstado",
                table: "Estados");

            migrationBuilder.DropTable(
                name: "CategoriasEstado");

            migrationBuilder.DropIndex(
                name: "IX_Estados_idCategoriaEstado",
                table: "Estados");

            migrationBuilder.DropColumn(
                name: "idCategoriaEstado",
                table: "Estados");
        }
    }
}
