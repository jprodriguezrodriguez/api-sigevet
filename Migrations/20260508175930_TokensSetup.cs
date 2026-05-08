using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sigevet.Migrations
{
    /// <inheritdoc />
    public partial class TokensSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    idRefreshToken = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tokenHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaExpiracion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaRevocacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ip = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    userAgent = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    idCuentaUsuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.idRefreshToken);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_CuentasUsuarios_idCuentaUsuario",
                        column: x => x.idCuentaUsuario,
                        principalTable: "CuentasUsuarios",
                        principalColumn: "idCuentaUsuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TokensCuentas",
                columns: table => new
                {
                    idToken = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tokenHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    fechaExpiracion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fechaUso = table.Column<DateTime>(type: "datetime2", nullable: true),
                    usado = table.Column<bool>(type: "bit", nullable: false),
                    idCuentaUsuario = table.Column<int>(type: "int", nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    fechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TokensCuentas", x => x.idToken);
                    table.ForeignKey(
                        name: "FK_TokensCuentas_CuentasUsuarios_idCuentaUsuario",
                        column: x => x.idCuentaUsuario,
                        principalTable: "CuentasUsuarios",
                        principalColumn: "idCuentaUsuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_idCuentaUsuario",
                table: "RefreshTokens",
                column: "idCuentaUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_TokensCuentas_idCuentaUsuario",
                table: "TokensCuentas",
                column: "idCuentaUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "TokensCuentas");
        }
    }
}
