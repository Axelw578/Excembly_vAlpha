using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Excembly_vAlpha.Migrations
{
    /// <inheritdoc />
    public partial class Actualizacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServiciosAdicionales",
                table: "Contratacion");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ServiciosAdicionales",
                table: "Contratacion",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
