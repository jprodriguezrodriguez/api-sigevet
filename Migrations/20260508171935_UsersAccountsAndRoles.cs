using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sigevet.Migrations
{
    /// <inheritdoc />
    public partial class UsersAccountsAndRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tutores_Estados_idEstadoCuentaTutor",
                table: "Tutores");

            migrationBuilder.RenameColumn(
                name: "idEstadoCuentaTutor",
                table: "Tutores",
                newName: "idEstadoTutor");

            migrationBuilder.RenameIndex(
                name: "IX_Tutores_idEstadoCuentaTutor",
                table: "Tutores",
                newName: "IX_Tutores_idEstadoTutor");

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    idRol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rolUsuario = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    fechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.idRol);
                });

            migrationBuilder.CreateTable(
                name: "CuentasUsuarios",
                columns: table => new
                {
                    idCuentaUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    passwordHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ultimoInicioSesion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    intentosFallidos = table.Column<int>(type: "int", nullable: false),
                    fechaDesbloqueo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    idPersona = table.Column<int>(type: "int", nullable: false),
                    idEstadoCuenta = table.Column<int>(type: "int", nullable: false),
                    idRol = table.Column<int>(type: "int", nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    fechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuentasUsuarios", x => x.idCuentaUsuario);
                    table.ForeignKey(
                        name: "FK_CuentasUsuarios_Estados_idEstadoCuenta",
                        column: x => x.idEstadoCuenta,
                        principalTable: "Estados",
                        principalColumn: "idEstado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CuentasUsuarios_Personas_idPersona",
                        column: x => x.idPersona,
                        principalTable: "Personas",
                        principalColumn: "idPersona",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CuentasUsuarios_Roles_idRol",
                        column: x => x.idRol,
                        principalTable: "Roles",
                        principalColumn: "idRol",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CuentasUsuarios_idEstadoCuenta",
                table: "CuentasUsuarios",
                column: "idEstadoCuenta");

            migrationBuilder.CreateIndex(
                name: "IX_CuentasUsuarios_idPersona",
                table: "CuentasUsuarios",
                column: "idPersona",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CuentasUsuarios_idRol",
                table: "CuentasUsuarios",
                column: "idRol");

            migrationBuilder.CreateIndex(
                name: "IX_CuentasUsuarios_username",
                table: "CuentasUsuarios",
                column: "username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_rolUsuario",
                table: "Roles",
                column: "rolUsuario",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tutores_Estados_idEstadoTutor",
                table: "Tutores",
                column: "idEstadoTutor",
                principalTable: "Estados",
                principalColumn: "idEstado");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tutores_Estados_idEstadoTutor",
                table: "Tutores");

            migrationBuilder.DropTable(
                name: "CuentasUsuarios");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.RenameColumn(
                name: "idEstadoTutor",
                table: "Tutores",
                newName: "idEstadoCuentaTutor");

            migrationBuilder.RenameIndex(
                name: "IX_Tutores_idEstadoTutor",
                table: "Tutores",
                newName: "IX_Tutores_idEstadoCuentaTutor");

            migrationBuilder.AddForeignKey(
                name: "FK_Tutores_Estados_idEstadoCuentaTutor",
                table: "Tutores",
                column: "idEstadoCuentaTutor",
                principalTable: "Estados",
                principalColumn: "idEstado");
        }
    }
}
