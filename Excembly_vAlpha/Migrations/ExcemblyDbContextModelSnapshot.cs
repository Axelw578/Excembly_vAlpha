﻿// <auto-generated />
using System;
using Excembly_vAlpha.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Excembly_vAlpha.Migrations
{
    [DbContext(typeof(ExcemblyDbContext))]
    partial class ExcemblyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Excembly_vAlpha.Models.AcercaDe", b =>
                {
                    b.Property<int>("AcercaDeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("AcercaDeId"));

                    b.Property<string>("Aclaracion")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("DomicilioTienda")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("HorarioTienda")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ImagenMapa")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("MisionVision")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PoliticaCancelacion")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("AcercaDeId");

                    b.ToTable("AcercaDe");
                });

            modelBuilder.Entity("Excembly_vAlpha.Models.AsignacionTecnico", b =>
                {
                    b.Property<int>("AsignacionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("AsignacionId"));

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("FechaAsignacion")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("ServicioId")
                        .HasColumnType("int");

                    b.Property<int>("TecnicoId")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("AsignacionId");

                    b.HasIndex("ServicioId");

                    b.HasIndex("TecnicoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("AsignacionesTecnicos");
                });

            modelBuilder.Entity("Excembly_vAlpha.Models.BitacoraEvento", b =>
                {
                    b.Property<int>("EventoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("EventoId"));

                    b.Property<string>("DescripcionEvento")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("FechaHora")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("IPUsuario")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("TipoEvento")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("EventoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("BitacoraEventos");
                });

            modelBuilder.Entity("Excembly_vAlpha.Models.Cita", b =>
                {
                    b.Property<int>("CitaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("CitaId"));

                    b.Property<string>("Comentarios")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("Domicilio")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("EstadoContratacion")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("FechaCita")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("FechaContratacion")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Imagen")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("TecnicoId")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("CitaId");

                    b.HasIndex("TecnicoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Citas");
                });

            modelBuilder.Entity("Excembly_vAlpha.Models.Direccion", b =>
                {
                    b.Property<int>("DireccionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("DireccionId"));

                    b.Property<string>("Calle")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Colonia")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("DescripcionEdificio")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("NumeroEdificio")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ReferenciaEdificio")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("DireccionId");

                    b.ToTable("Direcciones");
                });

            modelBuilder.Entity("Excembly_vAlpha.Models.DispositivoPlanFamiliar", b =>
                {
                    b.Property<int>("DispositivoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("DispositivoId"));

                    b.Property<bool>("EquipoAdicional")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("MACAddress")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("PlanId")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("DispositivoId");

                    b.HasIndex("PlanId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("DispositivosPlanFamiliar");
                });

            modelBuilder.Entity("Excembly_vAlpha.Models.Pago", b =>
                {
                    b.Property<int>("PagoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("PagoId"));

                    b.Property<int>("CitaId")
                        .HasColumnType("int");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("FechaPago")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("MetodoPago")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("Monto")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Referencia")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("PagoId");

                    b.HasIndex("CitaId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Pagos");
                });

            modelBuilder.Entity("Excembly_vAlpha.Models.Plan", b =>
                {
                    b.Property<int>("PlanId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("PlanId"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Imagen")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("Precio")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("PlanId");

                    b.ToTable("Planes");
                });

            modelBuilder.Entity("Excembly_vAlpha.Models.PlanPersonalizado", b =>
                {
                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.Property<int>("CitaId")
                        .HasColumnType("int");

                    b.Property<int>("ServicioId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaContratacion")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("TipoServicio")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("UsuarioId", "CitaId", "ServicioId");

                    b.HasIndex("CitaId");

                    b.HasIndex("ServicioId");

                    b.ToTable("PlanesPersonalizados");
                });

            modelBuilder.Entity("Excembly_vAlpha.Models.PlanServicio", b =>
                {
                    b.Property<int>("PlanId")
                        .HasColumnType("int");

                    b.Property<int>("ServicioId")
                        .HasColumnType("int");

                    b.HasKey("PlanId", "ServicioId");

                    b.HasIndex("ServicioId");

                    b.ToTable("PlanesServicios");
                });

            modelBuilder.Entity("Excembly_vAlpha.Models.PoliticaPrivacidad", b =>
                {
                    b.Property<int>("PoliticaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("PoliticaId"));

                    b.Property<string>("Contenido")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("PoliticaId");

                    b.ToTable("PoliticasPrivacidad");
                });

            modelBuilder.Entity("Excembly_vAlpha.Models.Rol", b =>
                {
                    b.Property<int>("RolId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("RolId"));

                    b.Property<string>("TipoRol")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("RolId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Excembly_vAlpha.Models.Servicio", b =>
                {
                    b.Property<int>("ServicioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("ServicioId"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("ExclusivoPaquete")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Imagen")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("Precio")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int?>("TipoServicioId")
                        .HasColumnType("int");

                    b.HasKey("ServicioId");

                    b.HasIndex("TipoServicioId");

                    b.ToTable("Servicios");
                });

            modelBuilder.Entity("Excembly_vAlpha.Models.ServicioAdicional", b =>
                {
                    b.Property<int>("PlanId")
                        .HasColumnType("int");

                    b.Property<int>("ServicioId")
                        .HasColumnType("int");

                    b.Property<decimal>("Descuento")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("PlanId", "ServicioId");

                    b.HasIndex("ServicioId");

                    b.ToTable("ServiciosAdicionales");
                });

            modelBuilder.Entity("Excembly_vAlpha.Models.TarjetaGuardada", b =>
                {
                    b.Property<int>("TarjetaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("TarjetaId"));

                    b.Property<string>("CVV")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("FechaExpiracion")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NombreTitular")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("NumeroTarjeta")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("TipoTarjeta")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("TarjetaId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("TarjetasGuardadas");
                });

            modelBuilder.Entity("Excembly_vAlpha.Models.Tecnico", b =>
                {
                    b.Property<int>("TecnicoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("TecnicoId"));

                    b.Property<string>("Apellidos")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("Disponibilidad")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("Edad")
                        .HasColumnType("int");

                    b.Property<string>("Experiencia")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Foto")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("TecnicoId");

                    b.ToTable("Tecnicos");
                });

            modelBuilder.Entity("Excembly_vAlpha.Models.TipoServicio", b =>
                {
                    b.Property<int>("TipoServicioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("TipoServicioId"));

                    b.Property<string>("NombreTipoServicio")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("TipoServicioId");

                    b.ToTable("TiposServicio");
                });

            modelBuilder.Entity("Excembly_vAlpha.Models.Trabajo", b =>
                {
                    b.Property<int>("TrabajoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("TrabajoId"));

                    b.Property<int>("CitaId")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("FechaTrabajo")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("ServicioId")
                        .HasColumnType("int");

                    b.Property<int>("TecnicoId")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("TrabajoId");

                    b.HasIndex("CitaId");

                    b.HasIndex("ServicioId");

                    b.HasIndex("TecnicoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Trabajos");
                });

            modelBuilder.Entity("Excembly_vAlpha.Models.Usuario", b =>
                {
                    b.Property<int>("UsuarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("UsuarioId"));

                    b.Property<string>("Apellidos")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Contraseña")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CorreoElectronico")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("DireccionId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("FotoPerfilUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("RolId")
                        .HasColumnType("int");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("UsuarioId");

                    b.HasIndex("DireccionId");

                    b.HasIndex("RolId");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Excembly_vAlpha.Models.AsignacionTecnico", b =>
                {
                    b.HasOne("Excembly_vAlpha.Models.Servicio", "Servicio")
                        .WithMany("AsignacionesTecnicos")
                        .HasForeignKey("ServicioId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Excembly_vAlpha.Models.Tecnico", "Tecnico")
                        .WithMany("AsignacionesTecnico")
                        .HasForeignKey("TecnicoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Excembly_vAlpha.Models.Usuario", "Usuario")
                        .WithMany("AsignacionesTecnico")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Servicio");

                    b.Navigation("Tecnico");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Excembly_vAlpha.Models.BitacoraEvento", b =>
                {
                    b.HasOne("Excembly_vAlpha.Models.Usuario", "Usuario")
                        .WithMany("BitacoraEventos")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Excembly_vAlpha.Models.Cita", b =>
                {
                    b.HasOne("Excembly_vAlpha.Models.Tecnico", "Tecnico")
                        .WithMany("Citas")
                        .HasForeignKey("TecnicoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Excembly_vAlpha.Models.Usuario", "Usuario")
                        .WithMany("Citas")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Tecnico");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Excembly_vAlpha.Models.DispositivoPlanFamiliar", b =>
                {
                    b.HasOne("Excembly_vAlpha.Models.Plan", "Plan")
                        .WithMany("DispositivosPlanFamiliar")
                        .HasForeignKey("PlanId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Excembly_vAlpha.Models.Usuario", "Usuario")
                        .WithMany("DispositivosPlanFamiliar")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Plan");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Excembly_vAlpha.Models.Pago", b =>
                {
                    b.HasOne("Excembly_vAlpha.Models.Cita", "Cita")
                        .WithMany("Pagos")
                        .HasForeignKey("CitaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Excembly_vAlpha.Models.Usuario", "Usuario")
                        .WithMany("Pagos")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Cita");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Excembly_vAlpha.Models.PlanPersonalizado", b =>
                {
                    b.HasOne("Excembly_vAlpha.Models.Cita", "Cita")
                        .WithMany("PlanesPersonalizados")
                        .HasForeignKey("CitaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Excembly_vAlpha.Models.Servicio", "Servicio")
                        .WithMany("PlanesPersonalizados")
                        .HasForeignKey("ServicioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Excembly_vAlpha.Models.Usuario", "Usuario")
                        .WithMany("PlanesPersonalizados")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cita");

                    b.Navigation("Servicio");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Excembly_vAlpha.Models.PlanServicio", b =>
                {
                    b.HasOne("Excembly_vAlpha.Models.Plan", "Plan")
                        .WithMany("PlanServicios")
                        .HasForeignKey("PlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Excembly_vAlpha.Models.Servicio", "Servicio")
                        .WithMany("PlanServicios")
                        .HasForeignKey("ServicioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Plan");

                    b.Navigation("Servicio");
                });

            modelBuilder.Entity("Excembly_vAlpha.Models.Servicio", b =>
                {
                    b.HasOne("Excembly_vAlpha.Models.TipoServicio", "TipoServicio")
                        .WithMany("Servicios")
                        .HasForeignKey("TipoServicioId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("TipoServicio");
                });

            modelBuilder.Entity("Excembly_vAlpha.Models.ServicioAdicional", b =>
                {
                    b.HasOne("Excembly_vAlpha.Models.Plan", "Plan")
                        .WithMany("ServiciosAdicionales")
                        .HasForeignKey("PlanId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Excembly_vAlpha.Models.Servicio", "Servicio")
                        .WithMany("ServiciosAdicionales")
                        .HasForeignKey("ServicioId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Plan");

                    b.Navigation("Servicio");
                });

            modelBuilder.Entity("Excembly_vAlpha.Models.TarjetaGuardada", b =>
                {
                    b.HasOne("Excembly_vAlpha.Models.Usuario", "Usuario")
                        .WithMany("TarjetasGuardadas")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Excembly_vAlpha.Models.Trabajo", b =>
                {
                    b.HasOne("Excembly_vAlpha.Models.Cita", "Cita")
                        .WithMany()
                        .HasForeignKey("CitaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Excembly_vAlpha.Models.Servicio", null)
                        .WithMany("Trabajos")
                        .HasForeignKey("ServicioId");

                    b.HasOne("Excembly_vAlpha.Models.Tecnico", "Tecnico")
                        .WithMany("Trabajos")
                        .HasForeignKey("TecnicoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Excembly_vAlpha.Models.Usuario", "Usuario")
                        .WithMany("Trabajos")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cita");

                    b.Navigation("Tecnico");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Excembly_vAlpha.Models.Usuario", b =>
                {
                    b.HasOne("Excembly_vAlpha.Models.Direccion", "Direccion")
                        .WithMany()
                        .HasForeignKey("DireccionId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Excembly_vAlpha.Models.Rol", "Rol")
                        .WithMany("Usuarios")
                        .HasForeignKey("RolId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Direccion");

                    b.Navigation("Rol");
                });

            modelBuilder.Entity("Excembly_vAlpha.Models.Cita", b =>
                {
                    b.Navigation("Pagos");

                    b.Navigation("PlanesPersonalizados");
                });

            modelBuilder.Entity("Excembly_vAlpha.Models.Plan", b =>
                {
                    b.Navigation("DispositivosPlanFamiliar");

                    b.Navigation("PlanServicios");

                    b.Navigation("ServiciosAdicionales");
                });

            modelBuilder.Entity("Excembly_vAlpha.Models.Rol", b =>
                {
                    b.Navigation("Usuarios");
                });

            modelBuilder.Entity("Excembly_vAlpha.Models.Servicio", b =>
                {
                    b.Navigation("AsignacionesTecnicos");

                    b.Navigation("PlanServicios");

                    b.Navigation("PlanesPersonalizados");

                    b.Navigation("ServiciosAdicionales");

                    b.Navigation("Trabajos");
                });

            modelBuilder.Entity("Excembly_vAlpha.Models.Tecnico", b =>
                {
                    b.Navigation("AsignacionesTecnico");

                    b.Navigation("Citas");

                    b.Navigation("Trabajos");
                });

            modelBuilder.Entity("Excembly_vAlpha.Models.TipoServicio", b =>
                {
                    b.Navigation("Servicios");
                });

            modelBuilder.Entity("Excembly_vAlpha.Models.Usuario", b =>
                {
                    b.Navigation("AsignacionesTecnico");

                    b.Navigation("BitacoraEventos");

                    b.Navigation("Citas");

                    b.Navigation("DispositivosPlanFamiliar");

                    b.Navigation("Pagos");

                    b.Navigation("PlanesPersonalizados");

                    b.Navigation("TarjetasGuardadas");

                    b.Navigation("Trabajos");
                });
#pragma warning restore 612, 618
        }
    }
}
