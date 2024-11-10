using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Excembly_vAlpha.Migrations
{
    /// <inheritdoc />
    public partial class Comentario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comentario",
                columns: table => new
                {
                    ComentarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    ContratacionId = table.Column<int>(type: "int", nullable: false),
                    Opinion = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaComentario = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FotoUrl = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comentario", x => x.ComentarioId);
                    table.ForeignKey(
                        name: "FK_Comentario_Contratacion_ContratacionId",
                        column: x => x.ContratacionId,
                        principalTable: "Contratacion",
                        principalColumn: "ContratacionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comentario_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Comentario_ContratacionId",
                table: "Comentario",
                column: "ContratacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Comentario_UsuarioId",
                table: "Comentario",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comentario");
        }
    }
}
