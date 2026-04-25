using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sigevet.Migrations
{
    /// <inheritdoc />
    public partial class BrigadaVeterinario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RolesParticipacion",
                columns: table => new
                {
                    idRolParticipacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rolParticipacion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolesParticipacion", x => x.idRolParticipacion);
                });

            migrationBuilder.CreateTable(
                name: "BrigadasVeterinario",
                columns: table => new
                {
                    idBrigadaVeterinario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idVeterinario = table.Column<int>(type: "int", nullable: false),
                    idBrigada = table.Column<int>(type: "int", nullable: false),
                    idRolParticipacion = table.Column<int>(type: "int", nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrigadasVeterinario", x => x.idBrigadaVeterinario);
                    table.ForeignKey(
                        name: "FK_BrigadasVeterinario_Brigadas_idBrigada",
                        column: x => x.idBrigada,
                        principalTable: "Brigadas",
                        principalColumn: "idBrigada",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BrigadasVeterinario_RolesParticipacion_idRolParticipacion",
                        column: x => x.idRolParticipacion,
                        principalTable: "RolesParticipacion",
                        principalColumn: "idRolParticipacion",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BrigadasVeterinario_Veterinarios_idVeterinario",
                        column: x => x.idVeterinario,
                        principalTable: "Veterinarios",
                        principalColumn: "idPersonaVet",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BrigadasVeterinario_idBrigada",
                table: "BrigadasVeterinario",
                column: "idBrigada");

            migrationBuilder.CreateIndex(
                name: "IX_BrigadasVeterinario_idRolParticipacion",
                table: "BrigadasVeterinario",
                column: "idRolParticipacion");

            migrationBuilder.CreateIndex(
                name: "IX_BrigadasVeterinario_idVeterinario",
                table: "BrigadasVeterinario",
                column: "idVeterinario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BrigadasVeterinario");

            migrationBuilder.DropTable(
                name: "RolesParticipacion");
        }
    }
}
