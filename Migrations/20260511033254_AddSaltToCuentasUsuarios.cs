using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sigevet.Migrations
{
    /// <inheritdoc />
    public partial class AddSaltToCuentasUsuarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "salt",
                table: "CuentasUsuarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "salt",
                table: "CuentasUsuarios");
        }
    }
}
