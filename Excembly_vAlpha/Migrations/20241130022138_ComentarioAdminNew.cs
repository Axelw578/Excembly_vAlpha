using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Excembly_vAlpha.Migrations
{
    /// <inheritdoc />
    public partial class ComentarioAdminNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentarios_Contratacion_ContratacionId",
                table: "Comentarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Comentarios_Usuarios_UsuarioId",
                table: "Comentarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comentarios",
                table: "Comentarios");

            migrationBuilder.RenameTable(
                name: "Comentarios",
                newName: "Comentario");

            migrationBuilder.RenameIndex(
                name: "IX_Comentarios_UsuarioId",
                table: "Comentario",
                newName: "IX_Comentario_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Comentarios_ContratacionId",
                table: "Comentario",
                newName: "IX_Comentario_ContratacionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comentario",
                table: "Comentario",
                column: "ComentarioId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentario_Contratacion_ContratacionId",
                table: "Comentario");

            migrationBuilder.DropForeignKey(
                name: "FK_Comentario_Usuarios_UsuarioId",
                table: "Comentario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comentario",
                table: "Comentario");

            migrationBuilder.RenameTable(
                name: "Comentario",
                newName: "Comentarios");

            migrationBuilder.RenameIndex(
                name: "IX_Comentario_UsuarioId",
                table: "Comentarios",
                newName: "IX_Comentarios_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Comentario_ContratacionId",
                table: "Comentarios",
                newName: "IX_Comentarios_ContratacionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comentarios",
                table: "Comentarios",
                column: "ComentarioId");

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
    }
}
