using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Excembly_vAlpha.Migrations
{
    /// <inheritdoc />
    public partial class Arreglo1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Citas_Direcciones_DireccionId",
                table: "Citas");

            migrationBuilder.DropForeignKey(
                name: "FK_Citas_Planes_PlanId",
                table: "Citas");

            migrationBuilder.DropForeignKey(
                name: "FK_Citas_Tecnicos_TecnicoId",
                table: "Citas");

            migrationBuilder.DropForeignKey(
                name: "FK_Citas_Usuarios_UsuarioId",
                table: "Citas");

            migrationBuilder.DropForeignKey(
                name: "FK_Contratacion_Citas_CitaId",
                table: "Contratacion");

            migrationBuilder.DropForeignKey(
                name: "FK_Pagos_Citas_CitaId",
                table: "Pagos");

            migrationBuilder.DropForeignKey(
                name: "FK_PlanPersonalizado_Citas_CitaId",
                table: "PlanPersonalizado");

            migrationBuilder.DropForeignKey(
                name: "FK_serviciosadicionales_Citas_CitaId",
                table: "serviciosadicionales");

            migrationBuilder.DropForeignKey(
                name: "FK_Trabajos_Citas_CitaId",
                table: "Trabajos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServicioAdicionalContratado",
                table: "ServicioAdicionalContratado");

            migrationBuilder.DropIndex(
                name: "IX_ServicioAdicionalContratado_ServicioAdicionalId_PlanId",
                table: "ServicioAdicionalContratado");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Citas",
                table: "Citas");

            migrationBuilder.RenameTable(
                name: "Citas",
                newName: "Cita");

            migrationBuilder.RenameIndex(
                name: "IX_Citas_UsuarioId",
                table: "Cita",
                newName: "IX_Cita_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Citas_TecnicoId",
                table: "Cita",
                newName: "IX_Cita_TecnicoId");

            migrationBuilder.RenameIndex(
                name: "IX_Citas_PlanId",
                table: "Cita",
                newName: "IX_Cita_PlanId");

            migrationBuilder.RenameIndex(
                name: "IX_Citas_DireccionId",
                table: "Cita",
                newName: "IX_Cita_DireccionId");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ServicioAdicionalContratado",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServicioAdicionalContratado",
                table: "ServicioAdicionalContratado",
                columns: new[] { "ServicioAdicionalId", "PlanId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cita",
                table: "Cita",
                column: "CitaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cita_Direcciones_DireccionId",
                table: "Cita",
                column: "DireccionId",
                principalTable: "Direcciones",
                principalColumn: "DireccionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cita_Planes_PlanId",
                table: "Cita",
                column: "PlanId",
                principalTable: "Planes",
                principalColumn: "PlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cita_Tecnicos_TecnicoId",
                table: "Cita",
                column: "TecnicoId",
                principalTable: "Tecnicos",
                principalColumn: "TecnicoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cita_Usuarios_UsuarioId",
                table: "Cita",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contratacion_Cita_CitaId",
                table: "Contratacion",
                column: "CitaId",
                principalTable: "Cita",
                principalColumn: "CitaId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pagos_Cita_CitaId",
                table: "Pagos",
                column: "CitaId",
                principalTable: "Cita",
                principalColumn: "CitaId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlanPersonalizado_Cita_CitaId",
                table: "PlanPersonalizado",
                column: "CitaId",
                principalTable: "Cita",
                principalColumn: "CitaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_serviciosadicionales_Cita_CitaId",
                table: "serviciosadicionales",
                column: "CitaId",
                principalTable: "Cita",
                principalColumn: "CitaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trabajos_Cita_CitaId",
                table: "Trabajos",
                column: "CitaId",
                principalTable: "Cita",
                principalColumn: "CitaId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cita_Direcciones_DireccionId",
                table: "Cita");

            migrationBuilder.DropForeignKey(
                name: "FK_Cita_Planes_PlanId",
                table: "Cita");

            migrationBuilder.DropForeignKey(
                name: "FK_Cita_Tecnicos_TecnicoId",
                table: "Cita");

            migrationBuilder.DropForeignKey(
                name: "FK_Cita_Usuarios_UsuarioId",
                table: "Cita");

            migrationBuilder.DropForeignKey(
                name: "FK_Contratacion_Cita_CitaId",
                table: "Contratacion");

            migrationBuilder.DropForeignKey(
                name: "FK_Pagos_Cita_CitaId",
                table: "Pagos");

            migrationBuilder.DropForeignKey(
                name: "FK_PlanPersonalizado_Cita_CitaId",
                table: "PlanPersonalizado");

            migrationBuilder.DropForeignKey(
                name: "FK_serviciosadicionales_Cita_CitaId",
                table: "serviciosadicionales");

            migrationBuilder.DropForeignKey(
                name: "FK_Trabajos_Cita_CitaId",
                table: "Trabajos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServicioAdicionalContratado",
                table: "ServicioAdicionalContratado");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cita",
                table: "Cita");

            migrationBuilder.RenameTable(
                name: "Cita",
                newName: "Citas");

            migrationBuilder.RenameIndex(
                name: "IX_Cita_UsuarioId",
                table: "Citas",
                newName: "IX_Citas_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Cita_TecnicoId",
                table: "Citas",
                newName: "IX_Citas_TecnicoId");

            migrationBuilder.RenameIndex(
                name: "IX_Cita_PlanId",
                table: "Citas",
                newName: "IX_Citas_PlanId");

            migrationBuilder.RenameIndex(
                name: "IX_Cita_DireccionId",
                table: "Citas",
                newName: "IX_Citas_DireccionId");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ServicioAdicionalContratado",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServicioAdicionalContratado",
                table: "ServicioAdicionalContratado",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Citas",
                table: "Citas",
                column: "CitaId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicioAdicionalContratado_ServicioAdicionalId_PlanId",
                table: "ServicioAdicionalContratado",
                columns: new[] { "ServicioAdicionalId", "PlanId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Citas_Direcciones_DireccionId",
                table: "Citas",
                column: "DireccionId",
                principalTable: "Direcciones",
                principalColumn: "DireccionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Citas_Planes_PlanId",
                table: "Citas",
                column: "PlanId",
                principalTable: "Planes",
                principalColumn: "PlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Citas_Tecnicos_TecnicoId",
                table: "Citas",
                column: "TecnicoId",
                principalTable: "Tecnicos",
                principalColumn: "TecnicoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Citas_Usuarios_UsuarioId",
                table: "Citas",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contratacion_Citas_CitaId",
                table: "Contratacion",
                column: "CitaId",
                principalTable: "Citas",
                principalColumn: "CitaId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pagos_Citas_CitaId",
                table: "Pagos",
                column: "CitaId",
                principalTable: "Citas",
                principalColumn: "CitaId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlanPersonalizado_Citas_CitaId",
                table: "PlanPersonalizado",
                column: "CitaId",
                principalTable: "Citas",
                principalColumn: "CitaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_serviciosadicionales_Citas_CitaId",
                table: "serviciosadicionales",
                column: "CitaId",
                principalTable: "Citas",
                principalColumn: "CitaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trabajos_Citas_CitaId",
                table: "Trabajos",
                column: "CitaId",
                principalTable: "Citas",
                principalColumn: "CitaId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
