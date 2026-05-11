using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sigevet.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFieldNams : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "idRefreshToken",
                table: "RefreshTokens",
                newName: "idRefrescarToken");

            migrationBuilder.RenameColumn(
                name: "username",
                table: "CuentasUsuarios",
                newName: "usuario");

            migrationBuilder.RenameColumn(
                name: "passwordHash",
                table: "CuentasUsuarios",
                newName: "contraseñaHash");

            migrationBuilder.RenameIndex(
                name: "IX_CuentasUsuarios_username",
                table: "CuentasUsuarios",
                newName: "IX_CuentasUsuarios_usuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "idRefrescarToken",
                table: "RefreshTokens",
                newName: "idRefreshToken");

            migrationBuilder.RenameColumn(
                name: "usuario",
                table: "CuentasUsuarios",
                newName: "username");

            migrationBuilder.RenameColumn(
                name: "contraseñaHash",
                table: "CuentasUsuarios",
                newName: "passwordHash");

            migrationBuilder.RenameIndex(
                name: "IX_CuentasUsuarios_usuario",
                table: "CuentasUsuarios",
                newName: "IX_CuentasUsuarios_username");
        }
    }
}
