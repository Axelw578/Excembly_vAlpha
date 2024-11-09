using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Excembly_vAlpha.Migrations
{
    /// <inheritdoc />
    public partial class AddCitaIdIfMissing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaContratacion",
                table: "Citas");

            migrationBuilder.RenameColumn(
                name: "EstadoContratacion",
                table: "Citas",
                newName: "MotivoCancelacion");

            migrationBuilder.AddColumn<string>(
                name: "Banco",
                table: "TarjetasGuardadas",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Marca",
                table: "TarjetasGuardadas",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "CitaId",
                table: "ServiciosAdicionales",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ServiciosAdicionales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PlanPersonalizadoId",
                table: "PlanesPersonalizados",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "CitaId",
                table: "Pagos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ContratacionId",
                table: "Pagos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlanId",
                table: "Pagos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ServicioId",
                table: "Pagos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TarjetaGuardadaTarjetaId",
                table: "Pagos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TarjetaId",
                table: "Pagos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ContratacionId",
                table: "Citas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DireccionId",
                table: "Citas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EstadoCita",
                table: "Citas",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCancelacion",
                table: "Citas",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCitaModificada",
                table: "Citas",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlanId",
                table: "Citas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Contratacion",
                columns: table => new
                {
                    ContratacionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    PlanId = table.Column<int>(type: "int", nullable: true),
                    ServicioId = table.Column<int>(type: "int", nullable: true),
                    PlanPersonalizadoId = table.Column<int>(type: "int", nullable: true),
                    CitaId = table.Column<int>(type: "int", nullable: true),
                    FechaContratacion = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Estado = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaCancelacion = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    PlanPersonalizadoUsuarioId = table.Column<int>(type: "int", nullable: false),
                    PlanPersonalizadoCitaId = table.Column<int>(type: "int", nullable: false),
                    PlanPersonalizadoServicioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contratacion", x => x.ContratacionId);
                    table.ForeignKey(
                        name: "FK_Contratacion_Citas_CitaId",
                        column: x => x.CitaId,
                        principalTable: "Citas",
                        principalColumn: "CitaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contratacion_PlanesPersonalizados_PlanPersonalizadoUsuarioId~",
                        columns: x => new { x.PlanPersonalizadoUsuarioId, x.PlanPersonalizadoCitaId, x.PlanPersonalizadoServicioId },
                        principalTable: "PlanesPersonalizados",
                        principalColumns: new[] { "UsuarioId", "CitaId", "ServicioId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contratacion_Planes_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Planes",
                        principalColumn: "PlanId");
                    table.ForeignKey(
                        name: "FK_Contratacion_Servicios_ServicioId",
                        column: x => x.ServicioId,
                        principalTable: "Servicios",
                        principalColumn: "ServicioId");
                    table.ForeignKey(
                        name: "FK_Contratacion_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ServicioAdicionalContratado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ContratacionId = table.Column<int>(type: "int", nullable: false),
                    ServicioAdicionalId = table.Column<int>(type: "int", nullable: false),
                    DescuentoAplicado = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    ServicioAdicionalPlanId = table.Column<int>(type: "int", nullable: false),
                    ServicioAdicionalServicioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicioAdicionalContratado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServicioAdicionalContratado_Contratacion_ContratacionId",
                        column: x => x.ContratacionId,
                        principalTable: "Contratacion",
                        principalColumn: "ContratacionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServicioAdicionalContratado_ServiciosAdicionales_ServicioAdi~",
                        columns: x => new { x.ServicioAdicionalPlanId, x.ServicioAdicionalServicioId },
                        principalTable: "ServiciosAdicionales",
                        principalColumns: new[] { "PlanId", "ServicioId" },
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ServiciosAdicionales_CitaId",
                table: "ServiciosAdicionales",
                column: "CitaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_ContratacionId",
                table: "Pagos",
                column: "ContratacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_TarjetaGuardadaTarjetaId",
                table: "Pagos",
                column: "TarjetaGuardadaTarjetaId");

            migrationBuilder.CreateIndex(
                name: "IX_Citas_DireccionId",
                table: "Citas",
                column: "DireccionId");

            migrationBuilder.CreateIndex(
                name: "IX_Citas_PlanId",
                table: "Citas",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Contratacion_CitaId",
                table: "Contratacion",
                column: "CitaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contratacion_PlanId",
                table: "Contratacion",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Contratacion_PlanPersonalizadoUsuarioId_PlanPersonalizadoCit~",
                table: "Contratacion",
                columns: new[] { "PlanPersonalizadoUsuarioId", "PlanPersonalizadoCitaId", "PlanPersonalizadoServicioId" });

            migrationBuilder.CreateIndex(
                name: "IX_Contratacion_ServicioId",
                table: "Contratacion",
                column: "ServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_Contratacion_UsuarioId",
                table: "Contratacion",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicioAdicionalContratado_ContratacionId",
                table: "ServicioAdicionalContratado",
                column: "ContratacionId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicioAdicionalContratado_ServicioAdicionalPlanId_Servicio~",
                table: "ServicioAdicionalContratado",
                columns: new[] { "ServicioAdicionalPlanId", "ServicioAdicionalServicioId" });

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
                name: "FK_Pagos_Contratacion_ContratacionId",
                table: "Pagos",
                column: "ContratacionId",
                principalTable: "Contratacion",
                principalColumn: "ContratacionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pagos_TarjetasGuardadas_TarjetaGuardadaTarjetaId",
                table: "Pagos",
                column: "TarjetaGuardadaTarjetaId",
                principalTable: "TarjetasGuardadas",
                principalColumn: "TarjetaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiciosAdicionales_Citas_CitaId",
                table: "ServiciosAdicionales",
                column: "CitaId",
                principalTable: "Citas",
                principalColumn: "CitaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Citas_Direcciones_DireccionId",
                table: "Citas");

            migrationBuilder.DropForeignKey(
                name: "FK_Citas_Planes_PlanId",
                table: "Citas");

            migrationBuilder.DropForeignKey(
                name: "FK_Pagos_Contratacion_ContratacionId",
                table: "Pagos");

            migrationBuilder.DropForeignKey(
                name: "FK_Pagos_TarjetasGuardadas_TarjetaGuardadaTarjetaId",
                table: "Pagos");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiciosAdicionales_Citas_CitaId",
                table: "ServiciosAdicionales");

            migrationBuilder.DropTable(
                name: "ServicioAdicionalContratado");

            migrationBuilder.DropTable(
                name: "Contratacion");

            migrationBuilder.DropIndex(
                name: "IX_ServiciosAdicionales_CitaId",
                table: "ServiciosAdicionales");

            migrationBuilder.DropIndex(
                name: "IX_Pagos_ContratacionId",
                table: "Pagos");

            migrationBuilder.DropIndex(
                name: "IX_Pagos_TarjetaGuardadaTarjetaId",
                table: "Pagos");

            migrationBuilder.DropIndex(
                name: "IX_Citas_DireccionId",
                table: "Citas");

            migrationBuilder.DropIndex(
                name: "IX_Citas_PlanId",
                table: "Citas");

            migrationBuilder.DropColumn(
                name: "Banco",
                table: "TarjetasGuardadas");

            migrationBuilder.DropColumn(
                name: "Marca",
                table: "TarjetasGuardadas");

            migrationBuilder.DropColumn(
                name: "CitaId",
                table: "ServiciosAdicionales");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ServiciosAdicionales");

            migrationBuilder.DropColumn(
                name: "PlanPersonalizadoId",
                table: "PlanesPersonalizados");

            migrationBuilder.DropColumn(
                name: "ContratacionId",
                table: "Pagos");

            migrationBuilder.DropColumn(
                name: "PlanId",
                table: "Pagos");

            migrationBuilder.DropColumn(
                name: "ServicioId",
                table: "Pagos");

            migrationBuilder.DropColumn(
                name: "TarjetaGuardadaTarjetaId",
                table: "Pagos");

            migrationBuilder.DropColumn(
                name: "TarjetaId",
                table: "Pagos");

            migrationBuilder.DropColumn(
                name: "ContratacionId",
                table: "Citas");

            migrationBuilder.DropColumn(
                name: "DireccionId",
                table: "Citas");

            migrationBuilder.DropColumn(
                name: "EstadoCita",
                table: "Citas");

            migrationBuilder.DropColumn(
                name: "FechaCancelacion",
                table: "Citas");

            migrationBuilder.DropColumn(
                name: "FechaCitaModificada",
                table: "Citas");

            migrationBuilder.DropColumn(
                name: "PlanId",
                table: "Citas");

            migrationBuilder.RenameColumn(
                name: "MotivoCancelacion",
                table: "Citas",
                newName: "EstadoContratacion");

            migrationBuilder.AlterColumn<int>(
                name: "CitaId",
                table: "Pagos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaContratacion",
                table: "Citas",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
