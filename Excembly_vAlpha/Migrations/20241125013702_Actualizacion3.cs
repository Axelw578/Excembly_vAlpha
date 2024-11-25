using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Excembly_vAlpha.Migrations
{
    /// <inheritdoc />
    public partial class Actualizacion3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contratacion_PlanesPersonalizados_PlanPersonalizadoUsuarioId~",
                table: "Contratacion");

            migrationBuilder.DropForeignKey(
                name: "FK_Contratacion_Planes_PlanId",
                table: "Contratacion");

            migrationBuilder.DropForeignKey(
                name: "FK_Contratacion_Servicios_ServicioId",
                table: "Contratacion");

            migrationBuilder.DropForeignKey(
                name: "FK_Contratacion_Usuarios_UsuarioId",
                table: "Contratacion");

            migrationBuilder.DropForeignKey(
                name: "FK_PlanesPersonalizados_Citas_CitaId",
                table: "PlanesPersonalizados");

            migrationBuilder.DropForeignKey(
                name: "FK_PlanesPersonalizados_Servicios_ServicioId",
                table: "PlanesPersonalizados");

            migrationBuilder.DropForeignKey(
                name: "FK_PlanesPersonalizados_Usuarios_UsuarioId",
                table: "PlanesPersonalizados");

            migrationBuilder.DropForeignKey(
                name: "FK_ServicioAdicionalContratado_serviciosadicionales_ServicioAdi~",
                table: "ServicioAdicionalContratado");

            migrationBuilder.DropIndex(
                name: "IX_ServicioAdicionalContratado_ServicioAdicionalPlanId_Servicio~",
                table: "ServicioAdicionalContratado");

            migrationBuilder.DropIndex(
                name: "IX_Contratacion_PlanPersonalizadoUsuarioId_PlanPersonalizadoCit~",
                table: "Contratacion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlanesPersonalizados",
                table: "PlanesPersonalizados");

            migrationBuilder.DropColumn(
                name: "ServicioAdicionalPlanId",
                table: "ServicioAdicionalContratado");

            migrationBuilder.DropColumn(
                name: "PlanPersonalizadoCitaId",
                table: "Contratacion");

            migrationBuilder.DropColumn(
                name: "PlanPersonalizadoServicioId",
                table: "Contratacion");

            migrationBuilder.DropColumn(
                name: "PlanPersonalizadoUsuarioId",
                table: "Contratacion");

            migrationBuilder.RenameTable(
                name: "PlanesPersonalizados",
                newName: "PlanPersonalizado");

            migrationBuilder.RenameColumn(
                name: "ServicioAdicionalServicioId",
                table: "ServicioAdicionalContratado",
                newName: "PlanId");

            migrationBuilder.RenameIndex(
                name: "IX_PlanesPersonalizados_ServicioId",
                table: "PlanPersonalizado",
                newName: "IX_PlanPersonalizado_ServicioId");

            migrationBuilder.RenameIndex(
                name: "IX_PlanesPersonalizados_CitaId",
                table: "PlanPersonalizado",
                newName: "IX_PlanPersonalizado_CitaId");

            migrationBuilder.AlterColumn<int>(
                name: "PlanPersonalizadoId",
                table: "PlanPersonalizado",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlanPersonalizado",
                table: "PlanPersonalizado",
                column: "PlanPersonalizadoId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicioAdicionalContratado_ServicioAdicionalId_PlanId",
                table: "ServicioAdicionalContratado",
                columns: new[] { "ServicioAdicionalId", "PlanId" });

            migrationBuilder.CreateIndex(
                name: "IX_Contratacion_PlanPersonalizadoId",
                table: "Contratacion",
                column: "PlanPersonalizadoId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanPersonalizado_UsuarioId",
                table: "PlanPersonalizado",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contratacion_PlanPersonalizado_PlanPersonalizadoId",
                table: "Contratacion",
                column: "PlanPersonalizadoId",
                principalTable: "PlanPersonalizado",
                principalColumn: "PlanPersonalizadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contratacion_Planes_PlanId",
                table: "Contratacion",
                column: "PlanId",
                principalTable: "Planes",
                principalColumn: "PlanId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contratacion_Servicios_ServicioId",
                table: "Contratacion",
                column: "ServicioId",
                principalTable: "Servicios",
                principalColumn: "ServicioId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contratacion_Usuarios_UsuarioId",
                table: "Contratacion",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlanPersonalizado_Citas_CitaId",
                table: "PlanPersonalizado",
                column: "CitaId",
                principalTable: "Citas",
                principalColumn: "CitaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlanPersonalizado_Servicios_ServicioId",
                table: "PlanPersonalizado",
                column: "ServicioId",
                principalTable: "Servicios",
                principalColumn: "ServicioId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlanPersonalizado_Usuarios_UsuarioId",
                table: "PlanPersonalizado",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServicioAdicionalContratado_serviciosadicionales_ServicioAdi~",
                table: "ServicioAdicionalContratado",
                columns: new[] { "ServicioAdicionalId", "PlanId" },
                principalTable: "serviciosadicionales",
                principalColumns: new[] { "PlanId", "ServicioId" },
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contratacion_PlanPersonalizado_PlanPersonalizadoId",
                table: "Contratacion");

            migrationBuilder.DropForeignKey(
                name: "FK_Contratacion_Planes_PlanId",
                table: "Contratacion");

            migrationBuilder.DropForeignKey(
                name: "FK_Contratacion_Servicios_ServicioId",
                table: "Contratacion");

            migrationBuilder.DropForeignKey(
                name: "FK_Contratacion_Usuarios_UsuarioId",
                table: "Contratacion");

            migrationBuilder.DropForeignKey(
                name: "FK_PlanPersonalizado_Citas_CitaId",
                table: "PlanPersonalizado");

            migrationBuilder.DropForeignKey(
                name: "FK_PlanPersonalizado_Servicios_ServicioId",
                table: "PlanPersonalizado");

            migrationBuilder.DropForeignKey(
                name: "FK_PlanPersonalizado_Usuarios_UsuarioId",
                table: "PlanPersonalizado");

            migrationBuilder.DropForeignKey(
                name: "FK_ServicioAdicionalContratado_serviciosadicionales_ServicioAdi~",
                table: "ServicioAdicionalContratado");

            migrationBuilder.DropIndex(
                name: "IX_ServicioAdicionalContratado_ServicioAdicionalId_PlanId",
                table: "ServicioAdicionalContratado");

            migrationBuilder.DropIndex(
                name: "IX_Contratacion_PlanPersonalizadoId",
                table: "Contratacion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlanPersonalizado",
                table: "PlanPersonalizado");

            migrationBuilder.DropIndex(
                name: "IX_PlanPersonalizado_UsuarioId",
                table: "PlanPersonalizado");

            migrationBuilder.RenameTable(
                name: "PlanPersonalizado",
                newName: "PlanesPersonalizados");

            migrationBuilder.RenameColumn(
                name: "PlanId",
                table: "ServicioAdicionalContratado",
                newName: "ServicioAdicionalServicioId");

            migrationBuilder.RenameIndex(
                name: "IX_PlanPersonalizado_ServicioId",
                table: "PlanesPersonalizados",
                newName: "IX_PlanesPersonalizados_ServicioId");

            migrationBuilder.RenameIndex(
                name: "IX_PlanPersonalizado_CitaId",
                table: "PlanesPersonalizados",
                newName: "IX_PlanesPersonalizados_CitaId");

            migrationBuilder.AddColumn<int>(
                name: "ServicioAdicionalPlanId",
                table: "ServicioAdicionalContratado",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PlanPersonalizadoCitaId",
                table: "Contratacion",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PlanPersonalizadoServicioId",
                table: "Contratacion",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PlanPersonalizadoUsuarioId",
                table: "Contratacion",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "PlanPersonalizadoId",
                table: "PlanesPersonalizados",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlanesPersonalizados",
                table: "PlanesPersonalizados",
                columns: new[] { "UsuarioId", "CitaId", "ServicioId" });

            migrationBuilder.CreateIndex(
                name: "IX_ServicioAdicionalContratado_ServicioAdicionalPlanId_Servicio~",
                table: "ServicioAdicionalContratado",
                columns: new[] { "ServicioAdicionalPlanId", "ServicioAdicionalServicioId" });

            migrationBuilder.CreateIndex(
                name: "IX_Contratacion_PlanPersonalizadoUsuarioId_PlanPersonalizadoCit~",
                table: "Contratacion",
                columns: new[] { "PlanPersonalizadoUsuarioId", "PlanPersonalizadoCitaId", "PlanPersonalizadoServicioId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Contratacion_PlanesPersonalizados_PlanPersonalizadoUsuarioId~",
                table: "Contratacion",
                columns: new[] { "PlanPersonalizadoUsuarioId", "PlanPersonalizadoCitaId", "PlanPersonalizadoServicioId" },
                principalTable: "PlanesPersonalizados",
                principalColumns: new[] { "UsuarioId", "CitaId", "ServicioId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contratacion_Planes_PlanId",
                table: "Contratacion",
                column: "PlanId",
                principalTable: "Planes",
                principalColumn: "PlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contratacion_Servicios_ServicioId",
                table: "Contratacion",
                column: "ServicioId",
                principalTable: "Servicios",
                principalColumn: "ServicioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contratacion_Usuarios_UsuarioId",
                table: "Contratacion",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlanesPersonalizados_Citas_CitaId",
                table: "PlanesPersonalizados",
                column: "CitaId",
                principalTable: "Citas",
                principalColumn: "CitaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlanesPersonalizados_Servicios_ServicioId",
                table: "PlanesPersonalizados",
                column: "ServicioId",
                principalTable: "Servicios",
                principalColumn: "ServicioId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlanesPersonalizados_Usuarios_UsuarioId",
                table: "PlanesPersonalizados",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServicioAdicionalContratado_serviciosadicionales_ServicioAdi~",
                table: "ServicioAdicionalContratado",
                columns: new[] { "ServicioAdicionalPlanId", "ServicioAdicionalServicioId" },
                principalTable: "serviciosadicionales",
                principalColumns: new[] { "PlanId", "ServicioId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
