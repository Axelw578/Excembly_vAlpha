using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Excembly_vAlpha.Migrations
{
    /// <inheritdoc />
    public partial class TipoServicio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServicioAdicionalContratado_ServiciosAdicionales_ServicioAdi~",
                table: "ServicioAdicionalContratado");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiciosAdicionales_Citas_CitaId",
                table: "ServiciosAdicionales");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiciosAdicionales_Planes_PlanId",
                table: "ServiciosAdicionales");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiciosAdicionales_Servicios_ServicioId",
                table: "ServiciosAdicionales");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiciosAdicionales",
                table: "ServiciosAdicionales");

            migrationBuilder.RenameTable(
                name: "ServiciosAdicionales",
                newName: "serviciosadicionales");

            migrationBuilder.RenameIndex(
                name: "IX_ServiciosAdicionales_ServicioId",
                table: "serviciosadicionales",
                newName: "IX_serviciosadicionales_ServicioId");

            migrationBuilder.RenameIndex(
                name: "IX_ServiciosAdicionales_CitaId",
                table: "serviciosadicionales",
                newName: "IX_serviciosadicionales_CitaId");

            migrationBuilder.AddColumn<string>(
                name: "TipoServicio",
                table: "Contratacion",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_serviciosadicionales",
                table: "serviciosadicionales",
                columns: new[] { "PlanId", "ServicioId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ServicioAdicionalContratado_serviciosadicionales_ServicioAdi~",
                table: "ServicioAdicionalContratado",
                columns: new[] { "ServicioAdicionalPlanId", "ServicioAdicionalServicioId" },
                principalTable: "serviciosadicionales",
                principalColumns: new[] { "PlanId", "ServicioId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_serviciosadicionales_Citas_CitaId",
                table: "serviciosadicionales",
                column: "CitaId",
                principalTable: "Citas",
                principalColumn: "CitaId");

            migrationBuilder.AddForeignKey(
                name: "FK_serviciosadicionales_Planes_PlanId",
                table: "serviciosadicionales",
                column: "PlanId",
                principalTable: "Planes",
                principalColumn: "PlanId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_serviciosadicionales_Servicios_ServicioId",
                table: "serviciosadicionales",
                column: "ServicioId",
                principalTable: "Servicios",
                principalColumn: "ServicioId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServicioAdicionalContratado_serviciosadicionales_ServicioAdi~",
                table: "ServicioAdicionalContratado");

            migrationBuilder.DropForeignKey(
                name: "FK_serviciosadicionales_Citas_CitaId",
                table: "serviciosadicionales");

            migrationBuilder.DropForeignKey(
                name: "FK_serviciosadicionales_Planes_PlanId",
                table: "serviciosadicionales");

            migrationBuilder.DropForeignKey(
                name: "FK_serviciosadicionales_Servicios_ServicioId",
                table: "serviciosadicionales");

            migrationBuilder.DropPrimaryKey(
                name: "PK_serviciosadicionales",
                table: "serviciosadicionales");

            migrationBuilder.DropColumn(
                name: "TipoServicio",
                table: "Contratacion");

            migrationBuilder.RenameTable(
                name: "serviciosadicionales",
                newName: "ServiciosAdicionales");

            migrationBuilder.RenameIndex(
                name: "IX_serviciosadicionales_ServicioId",
                table: "ServiciosAdicionales",
                newName: "IX_ServiciosAdicionales_ServicioId");

            migrationBuilder.RenameIndex(
                name: "IX_serviciosadicionales_CitaId",
                table: "ServiciosAdicionales",
                newName: "IX_ServiciosAdicionales_CitaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiciosAdicionales",
                table: "ServiciosAdicionales",
                columns: new[] { "PlanId", "ServicioId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ServicioAdicionalContratado_ServiciosAdicionales_ServicioAdi~",
                table: "ServicioAdicionalContratado",
                columns: new[] { "ServicioAdicionalPlanId", "ServicioAdicionalServicioId" },
                principalTable: "ServiciosAdicionales",
                principalColumns: new[] { "PlanId", "ServicioId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiciosAdicionales_Citas_CitaId",
                table: "ServiciosAdicionales",
                column: "CitaId",
                principalTable: "Citas",
                principalColumn: "CitaId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiciosAdicionales_Planes_PlanId",
                table: "ServiciosAdicionales",
                column: "PlanId",
                principalTable: "Planes",
                principalColumn: "PlanId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiciosAdicionales_Servicios_ServicioId",
                table: "ServiciosAdicionales",
                column: "ServicioId",
                principalTable: "Servicios",
                principalColumn: "ServicioId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
