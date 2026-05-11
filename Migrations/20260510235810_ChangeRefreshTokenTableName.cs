using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sigevet.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRefreshTokenTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_CuentasUsuarios_idCuentaUsuario",
                table: "RefreshTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RefreshTokens",
                table: "RefreshTokens");

            migrationBuilder.RenameTable(
                name: "RefreshTokens",
                newName: "RefrescarTokens");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshTokens_idCuentaUsuario",
                table: "RefrescarTokens",
                newName: "IX_RefrescarTokens_idCuentaUsuario");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefrescarTokens",
                table: "RefrescarTokens",
                column: "idRefrescarToken");

            migrationBuilder.AddForeignKey(
                name: "FK_RefrescarTokens_CuentasUsuarios_idCuentaUsuario",
                table: "RefrescarTokens",
                column: "idCuentaUsuario",
                principalTable: "CuentasUsuarios",
                principalColumn: "idCuentaUsuario",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefrescarTokens_CuentasUsuarios_idCuentaUsuario",
                table: "RefrescarTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RefrescarTokens",
                table: "RefrescarTokens");

            migrationBuilder.RenameTable(
                name: "RefrescarTokens",
                newName: "RefreshTokens");

            migrationBuilder.RenameIndex(
                name: "IX_RefrescarTokens_idCuentaUsuario",
                table: "RefreshTokens",
                newName: "IX_RefreshTokens_idCuentaUsuario");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefreshTokens",
                table: "RefreshTokens",
                column: "idRefrescarToken");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_CuentasUsuarios_idCuentaUsuario",
                table: "RefreshTokens",
                column: "idCuentaUsuario",
                principalTable: "CuentasUsuarios",
                principalColumn: "idCuentaUsuario",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
