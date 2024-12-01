using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Excembly_vAlpha.Migrations
{
    /// <inheritdoc />
    public partial class ComentarioGetSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropForeignKey(
                name: "FK_Comentario_Contratacion_ContratacionId",
                table: "Comentario");

            migrationBuilder.DropForeignKey(
                name: "FK_Comentario_Usuarios_UsuarioId",
                table: "Comentario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comentario",
                table: "Comentario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AsignacionTecnico",
                table: "AsignacionTecnico");

            migrationBuilder.RenameTable(
                name: "Comentario",
                newName: "Comentarios");

            migrationBuilder.RenameTable(
                name: "AsignacionTecnico",
                newName: "AsignacionesTecnicos");

            migrationBuilder.RenameIndex(
                name: "IX_Comentario_UsuarioId",
                table: "Comentarios",
                newName: "IX_Comentarios_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Comentario_ContratacionId",
                table: "Comentarios",
                newName: "IX_Comentarios_ContratacionId");

            migrationBuilder.RenameIndex(
                name: "IX_AsignacionTecnico_UsuarioId",
                table: "AsignacionesTecnicos",
                newName: "IX_AsignacionesTecnicos_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_AsignacionTecnico_TecnicoId1",
                table: "AsignacionesTecnicos",
                newName: "IX_AsignacionesTecnicos_TecnicoId1");

            migrationBuilder.RenameIndex(
                name: "IX_AsignacionTecnico_TecnicoId",
                table: "AsignacionesTecnicos",
                newName: "IX_AsignacionesTecnicos_TecnicoId");

            migrationBuilder.RenameIndex(
                name: "IX_AsignacionTecnico_ServicioId",
                table: "AsignacionesTecnicos",
                newName: "IX_AsignacionesTecnicos_ServicioId");

            migrationBuilder.RenameIndex(
                name: "IX_AsignacionTecnico_ContratacionId",
                table: "AsignacionesTecnicos",
                newName: "IX_AsignacionesTecnicos_ContratacionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comentarios",
                table: "Comentarios",
                column: "ComentarioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AsignacionesTecnicos",
                table: "AsignacionesTecnicos",
                column: "AsignacionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AsignacionesTecnicos_Contratacion_ContratacionId",
                table: "AsignacionesTecnicos",
                column: "ContratacionId",
                principalTable: "Contratacion",
                principalColumn: "ContratacionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AsignacionesTecnicos_Servicios_ServicioId",
                table: "AsignacionesTecnicos",
                column: "ServicioId",
                principalTable: "Servicios",
                principalColumn: "ServicioId");

            migrationBuilder.AddForeignKey(
                name: "FK_AsignacionesTecnicos_Tecnicos_TecnicoId",
                table: "AsignacionesTecnicos",
                column: "TecnicoId",
                principalTable: "Tecnicos",
                principalColumn: "TecnicoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AsignacionesTecnicos_Tecnicos_TecnicoId1",
                table: "AsignacionesTecnicos",
                column: "TecnicoId1",
                principalTable: "Tecnicos",
                principalColumn: "TecnicoId");

            migrationBuilder.AddForeignKey(
                name: "FK_AsignacionesTecnicos_Usuarios_UsuarioId",
                table: "AsignacionesTecnicos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comentarios_Contratacion_ContratacionId",
                table: "Comentarios",
                column: "ContratacionId",
                principalTable: "Contratacion",
                principalColumn: "ContratacionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comentarios_Usuarios_UsuarioId",
                table: "Comentarios",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AsignacionesTecnicos_Contratacion_ContratacionId",
                table: "AsignacionesTecnicos");

            migrationBuilder.DropForeignKey(
                name: "FK_AsignacionesTecnicos_Servicios_ServicioId",
                table: "AsignacionesTecnicos");

            migrationBuilder.DropForeignKey(
                name: "FK_AsignacionesTecnicos_Tecnicos_TecnicoId",
                table: "AsignacionesTecnicos");

            migrationBuilder.DropForeignKey(
                name: "FK_AsignacionesTecnicos_Tecnicos_TecnicoId1",
                table: "AsignacionesTecnicos");

            migrationBuilder.DropForeignKey(
                name: "FK_AsignacionesTecnicos_Usuarios_UsuarioId",
                table: "AsignacionesTecnicos");

            migrationBuilder.DropForeignKey(
                name: "FK_Comentarios_Contratacion_ContratacionId",
                table: "Comentarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Comentarios_Usuarios_UsuarioId",
                table: "Comentarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comentarios",
                table: "Comentarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AsignacionesTecnicos",
                table: "AsignacionesTecnicos");

            migrationBuilder.RenameTable(
                name: "Comentarios",
                newName: "Comentario");

            migrationBuilder.RenameTable(
                name: "AsignacionesTecnicos",
                newName: "AsignacionTecnico");

            migrationBuilder.RenameIndex(
                name: "IX_Comentarios_UsuarioId",
                table: "Comentario",
                newName: "IX_Comentario_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Comentarios_ContratacionId",
                table: "Comentario",
                newName: "IX_Comentario_ContratacionId");

            migrationBuilder.RenameIndex(
                name: "IX_AsignacionesTecnicos_UsuarioId",
                table: "AsignacionTecnico",
                newName: "IX_AsignacionTecnico_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_AsignacionesTecnicos_TecnicoId1",
                table: "AsignacionTecnico",
                newName: "IX_AsignacionTecnico_TecnicoId1");

            migrationBuilder.RenameIndex(
                name: "IX_AsignacionesTecnicos_TecnicoId",
                table: "AsignacionTecnico",
                newName: "IX_AsignacionTecnico_TecnicoId");

            migrationBuilder.RenameIndex(
                name: "IX_AsignacionesTecnicos_ServicioId",
                table: "AsignacionTecnico",
                newName: "IX_AsignacionTecnico_ServicioId");

            migrationBuilder.RenameIndex(
                name: "IX_AsignacionesTecnicos_ContratacionId",
                table: "AsignacionTecnico",
                newName: "IX_AsignacionTecnico_ContratacionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comentario",
                table: "Comentario",
                column: "ComentarioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AsignacionTecnico",
                table: "AsignacionTecnico",
                column: "AsignacionId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Comentario_Contratacion_ContratacionId",
                table: "Comentario",
                column: "ContratacionId",
                principalTable: "Contratacion",
                principalColumn: "ContratacionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comentario_Usuarios_UsuarioId",
                table: "Comentario",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
