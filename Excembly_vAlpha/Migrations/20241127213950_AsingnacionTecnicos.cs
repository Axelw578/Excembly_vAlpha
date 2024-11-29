using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Excembly_vAlpha.Migrations
{
    /// <inheritdoc />
    public partial class AsingnacionTecnicos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AsignacionesTecnicos_Servicios_ServicioId",
                table: "AsignacionesTecnicos");

            migrationBuilder.DropForeignKey(
                name: "FK_AsignacionesTecnicos_Tecnicos_TecnicoId",
                table: "AsignacionesTecnicos");

            migrationBuilder.DropForeignKey(
                name: "FK_AsignacionesTecnicos_Usuarios_UsuarioId",
                table: "AsignacionesTecnicos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AsignacionesTecnicos",
                table: "AsignacionesTecnicos");

            migrationBuilder.RenameTable(
                name: "AsignacionesTecnicos",
                newName: "AsignacionTecnico");

            migrationBuilder.RenameIndex(
                name: "IX_AsignacionesTecnicos_UsuarioId",
                table: "AsignacionTecnico",
                newName: "IX_AsignacionTecnico_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_AsignacionesTecnicos_TecnicoId",
                table: "AsignacionTecnico",
                newName: "IX_AsignacionTecnico_TecnicoId");

            migrationBuilder.RenameIndex(
                name: "IX_AsignacionesTecnicos_ServicioId",
                table: "AsignacionTecnico",
                newName: "IX_AsignacionTecnico_ServicioId");

            migrationBuilder.AlterColumn<int>(
                name: "ServicioId",
                table: "AsignacionTecnico",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ContratacionId",
                table: "AsignacionTecnico",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TecnicoId1",
                table: "AsignacionTecnico",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AsignacionTecnico",
                table: "AsignacionTecnico",
                column: "AsignacionId");

            migrationBuilder.CreateIndex(
                name: "IX_AsignacionTecnico_ContratacionId",
                table: "AsignacionTecnico",
                column: "ContratacionId");

            migrationBuilder.CreateIndex(
                name: "IX_AsignacionTecnico_TecnicoId1",
                table: "AsignacionTecnico",
                column: "TecnicoId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AsignacionTecnico_Contratacion_ContratacionId",
                table: "AsignacionTecnico",
                column: "ContratacionId",
                principalTable: "Contratacion",
                principalColumn: "ContratacionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AsignacionTecnico_Servicios_ServicioId",
                table: "AsignacionTecnico",
                column: "ServicioId",
                principalTable: "Servicios",
                principalColumn: "ServicioId");

            migrationBuilder.AddForeignKey(
                name: "FK_AsignacionTecnico_Tecnicos_TecnicoId",
                table: "AsignacionTecnico",
                column: "TecnicoId",
                principalTable: "Tecnicos",
                principalColumn: "TecnicoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AsignacionTecnico_Tecnicos_TecnicoId1",
                table: "AsignacionTecnico",
                column: "TecnicoId1",
                principalTable: "Tecnicos",
                principalColumn: "TecnicoId");

            migrationBuilder.AddForeignKey(
                name: "FK_AsignacionTecnico_Usuarios_UsuarioId",
                table: "AsignacionTecnico",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AsignacionTecnico_Contratacion_ContratacionId",
                table: "AsignacionTecnico");

            migrationBuilder.DropForeignKey(
                name: "FK_AsignacionTecnico_Servicios_ServicioId",
                table: "AsignacionTecnico");

            migrationBuilder.DropForeignKey(
                name: "FK_AsignacionTecnico_Tecnicos_TecnicoId",
                table: "AsignacionTecnico");

            migrationBuilder.DropForeignKey(
                name: "FK_AsignacionTecnico_Tecnicos_TecnicoId1",
                table: "AsignacionTecnico");

            migrationBuilder.DropForeignKey(
                name: "FK_AsignacionTecnico_Usuarios_UsuarioId",
                table: "AsignacionTecnico");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AsignacionTecnico",
                table: "AsignacionTecnico");

            migrationBuilder.DropIndex(
                name: "IX_AsignacionTecnico_ContratacionId",
                table: "AsignacionTecnico");

            migrationBuilder.DropIndex(
                name: "IX_AsignacionTecnico_TecnicoId1",
                table: "AsignacionTecnico");

            migrationBuilder.DropColumn(
                name: "ContratacionId",
                table: "AsignacionTecnico");

            migrationBuilder.DropColumn(
                name: "TecnicoId1",
                table: "AsignacionTecnico");

            migrationBuilder.RenameTable(
                name: "AsignacionTecnico",
                newName: "AsignacionesTecnicos");

            migrationBuilder.RenameIndex(
                name: "IX_AsignacionTecnico_UsuarioId",
                table: "AsignacionesTecnicos",
                newName: "IX_AsignacionesTecnicos_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_AsignacionTecnico_TecnicoId",
                table: "AsignacionesTecnicos",
                newName: "IX_AsignacionesTecnicos_TecnicoId");

            migrationBuilder.RenameIndex(
                name: "IX_AsignacionTecnico_ServicioId",
                table: "AsignacionesTecnicos",
                newName: "IX_AsignacionesTecnicos_ServicioId");

            migrationBuilder.AlterColumn<int>(
                name: "ServicioId",
                table: "AsignacionesTecnicos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AsignacionesTecnicos",
                table: "AsignacionesTecnicos",
                column: "AsignacionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AsignacionesTecnicos_Servicios_ServicioId",
                table: "AsignacionesTecnicos",
                column: "ServicioId",
                principalTable: "Servicios",
                principalColumn: "ServicioId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AsignacionesTecnicos_Tecnicos_TecnicoId",
                table: "AsignacionesTecnicos",
                column: "TecnicoId",
                principalTable: "Tecnicos",
                principalColumn: "TecnicoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AsignacionesTecnicos_Usuarios_UsuarioId",
                table: "AsignacionesTecnicos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
