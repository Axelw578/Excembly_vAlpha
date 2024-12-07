using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Excembly_vAlpha.Migrations
{
    /// <inheritdoc />
    public partial class TarjetaGuardadaUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pagos_TarjetasGuardadas_TarjetaGuardadaTarjetaId",
                table: "Pagos");

            migrationBuilder.DropIndex(
                name: "IX_Pagos_TarjetaGuardadaTarjetaId",
                table: "Pagos");

            migrationBuilder.DropColumn(
                name: "TarjetaGuardadaTarjetaId",
                table: "Pagos");

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_TarjetaId",
                table: "Pagos",
                column: "TarjetaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pagos_TarjetasGuardadas_TarjetaId",
                table: "Pagos",
                column: "TarjetaId",
                principalTable: "TarjetasGuardadas",
                principalColumn: "TarjetaId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pagos_TarjetasGuardadas_TarjetaId",
                table: "Pagos");

            migrationBuilder.DropIndex(
                name: "IX_Pagos_TarjetaId",
                table: "Pagos");

            migrationBuilder.AddColumn<int>(
                name: "TarjetaGuardadaTarjetaId",
                table: "Pagos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_TarjetaGuardadaTarjetaId",
                table: "Pagos",
                column: "TarjetaGuardadaTarjetaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pagos_TarjetasGuardadas_TarjetaGuardadaTarjetaId",
                table: "Pagos",
                column: "TarjetaGuardadaTarjetaId",
                principalTable: "TarjetasGuardadas",
                principalColumn: "TarjetaId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
