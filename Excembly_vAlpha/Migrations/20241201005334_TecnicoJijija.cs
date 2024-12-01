using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Excembly_vAlpha.Migrations
{
    /// <inheritdoc />
    public partial class TecnicoJijija : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AsignacionesTecnicos_Tecnicos_TecnicoId1",
                table: "AsignacionesTecnicos");

            migrationBuilder.DropIndex(
                name: "IX_AsignacionesTecnicos_TecnicoId1",
                table: "AsignacionesTecnicos");

            migrationBuilder.DropColumn(
                name: "TecnicoId1",
                table: "AsignacionesTecnicos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TecnicoId1",
                table: "AsignacionesTecnicos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AsignacionesTecnicos_TecnicoId1",
                table: "AsignacionesTecnicos",
                column: "TecnicoId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AsignacionesTecnicos_Tecnicos_TecnicoId1",
                table: "AsignacionesTecnicos",
                column: "TecnicoId1",
                principalTable: "Tecnicos",
                principalColumn: "TecnicoId");
        }
    }
}
