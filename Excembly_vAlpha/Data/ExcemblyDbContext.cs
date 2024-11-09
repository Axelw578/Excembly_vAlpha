using Excembly_vAlpha.Models;
using Microsoft.EntityFrameworkCore;

namespace Excembly_vAlpha.Data
{
    public class ExcemblyDbContext : DbContext
    {
        public ExcemblyDbContext(DbContextOptions<ExcemblyDbContext> options)
            : base(options)
        {


        }



        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Direccion> Direcciones { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<TipoServicio> TiposServicio { get; set; }
        public DbSet<Plan> Planes { get; set; }
        public DbSet<PlanServicio> PlanesServicios { get; set; }
        public DbSet<ServicioAdicional> ServiciosAdicionales { get; set; }
        public DbSet<PlanPersonalizado> PlanesPersonalizados { get; set; }
        public DbSet<Tecnico> Tecnicos { get; set; }
        public DbSet<Trabajo> Trabajos { get; set; }
        public DbSet<Cita> Citas { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<DispositivoPlanFamiliar> DispositivosPlanFamiliar { get; set; }
        public DbSet<TarjetaGuardada> TarjetasGuardadas { get; set; }
        public DbSet<BitacoraEvento> BitacoraEventos { get; set; }
        public DbSet<AcercaDe> AcercaDe { get; set; }
        public DbSet<PoliticaPrivacidad> PoliticasPrivacidad { get; set; }
        public DbSet<AsignacionTecnico> AsignacionesTecnicos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            // Configuración de la relación uno a uno entre Cita y Contratacion
            modelBuilder.Entity<Cita>()
                .HasOne(c => c.Contratacion) // Cita tiene una Contratacion
                .WithOne(co => co.Cita)      // Contratacion tiene una Cita
                .HasForeignKey<Contratacion>(co => co.CitaId) // Especifica que Contratacion tiene la clave foránea
                .OnDelete(DeleteBehavior.Restrict); // Define el comportamiento de eliminación

            // Configuración de Usuario
            modelBuilder.Entity<Usuario>()
                .HasKey(u => u.UsuarioId);
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Direccion)
                .WithMany()
                .HasForeignKey(u => u.DireccionId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Rol)
                .WithMany(r => r.Usuarios)
                .HasForeignKey(u => u.RolId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de Direccion
            modelBuilder.Entity<Direccion>()
                .HasKey(d => d.DireccionId);

            // Configuración de Rol
            modelBuilder.Entity<Rol>()
                .HasKey(r => r.RolId);

            // Configuración de Servicio
            modelBuilder.Entity<Servicio>()
                .HasKey(s => s.ServicioId);
            modelBuilder.Entity<Servicio>()
                .HasOne(s => s.TipoServicio)
                .WithMany(ts => ts.Servicios)
                .HasForeignKey(s => s.TipoServicioId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de TipoServicio
            modelBuilder.Entity<TipoServicio>()
                .HasKey(ts => ts.TipoServicioId);

            // Configuración de Plan
            modelBuilder.Entity<Plan>()
                .HasKey(p => p.PlanId);

            // Configuración de PlanServicio (Llave Compuesta)
            modelBuilder.Entity<PlanServicio>()
                .HasKey(ps => new { ps.PlanId, ps.ServicioId });
            modelBuilder.Entity<PlanServicio>()
                .HasOne(ps => ps.Plan)
                .WithMany(p => p.PlanServicios)
                .HasForeignKey(ps => ps.PlanId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<PlanServicio>()
                .HasOne(ps => ps.Servicio)
                .WithMany(s => s.PlanServicios)
                .HasForeignKey(ps => ps.ServicioId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuración de ServicioAdicional (Llave Compuesta)
            modelBuilder.Entity<ServicioAdicional>()
                .HasKey(sa => new { sa.PlanId, sa.ServicioId });
            modelBuilder.Entity<ServicioAdicional>()
                .HasOne(sa => sa.Plan)
                .WithMany(p => p.ServiciosAdicionales)
                .HasForeignKey(sa => sa.PlanId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ServicioAdicional>()
                .HasOne(sa => sa.Servicio)
                .WithMany(s => s.ServiciosAdicionales)
                .HasForeignKey(sa => sa.ServicioId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de PlanPersonalizado (Llave Compuesta)
            modelBuilder.Entity<PlanPersonalizado>()
                .HasKey(pp => new { pp.UsuarioId, pp.CitaId, pp.ServicioId });
            modelBuilder.Entity<PlanPersonalizado>()
                .HasOne(pp => pp.Usuario)
                .WithMany(u => u.PlanesPersonalizados)
                .HasForeignKey(pp => pp.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<PlanPersonalizado>()
                .HasOne(pp => pp.Cita)
                .WithMany(c => c.PlanesPersonalizados)
                .HasForeignKey(pp => pp.CitaId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<PlanPersonalizado>()
                .HasOne(pp => pp.Servicio)
                .WithMany(s => s.PlanesPersonalizados)
                .HasForeignKey(pp => pp.ServicioId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuración de Técnico
            modelBuilder.Entity<Tecnico>()
                .HasKey(t => t.TecnicoId);

            // Configuración de Cita
            modelBuilder.Entity<Cita>()
                .HasKey(c => c.CitaId);
            modelBuilder.Entity<Cita>()
                .HasOne(c => c.Usuario)
                .WithMany(u => u.Citas)
                .HasForeignKey(c => c.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Cita>()
                .HasOne(c => c.Tecnico)
                .WithMany(t => t.Citas)
                .HasForeignKey(c => c.TecnicoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de Pago
            modelBuilder.Entity<Pago>()
                .HasKey(p => p.PagoId);
            modelBuilder.Entity<Pago>()
                .HasOne(p => p.Usuario)
                .WithMany(u => u.Pagos)
                .HasForeignKey(p => p.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Pago>()
                .HasOne(p => p.Cita)
                .WithMany(c => c.Pagos)
                .HasForeignKey(p => p.CitaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de DispositivoPlanFamiliar
            modelBuilder.Entity<DispositivoPlanFamiliar>()
                .HasKey(dpf => dpf.DispositivoId);
            modelBuilder.Entity<DispositivoPlanFamiliar>()
                .HasOne(dpf => dpf.Usuario)
                .WithMany(u => u.DispositivosPlanFamiliar)
                .HasForeignKey(dpf => dpf.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<DispositivoPlanFamiliar>()
                .HasOne(dpf => dpf.Plan)
                .WithMany(p => p.DispositivosPlanFamiliar)
                .HasForeignKey(dpf => dpf.PlanId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de TarjetaGuardada
            modelBuilder.Entity<TarjetaGuardada>()
                .HasKey(tg => tg.TarjetaId);
            modelBuilder.Entity<TarjetaGuardada>()
                .HasOne(tg => tg.Usuario)
                .WithMany(u => u.TarjetasGuardadas)
                .HasForeignKey(tg => tg.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuración de BitacoraEvento
            modelBuilder.Entity<BitacoraEvento>()
                .HasKey(be => be.EventoId);
            modelBuilder.Entity<BitacoraEvento>()
                .HasOne(be => be.Usuario)
                .WithMany(u => u.BitacoraEventos)
                .HasForeignKey(be => be.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuración de AcercaDe
            modelBuilder.Entity<AcercaDe>()
                .HasKey(ad => ad.AcercaDeId);

            // Configuración de PoliticaPrivacidad
            modelBuilder.Entity<PoliticaPrivacidad>()
                .HasKey(pp => pp.PoliticaId);

            // Configuración de AsignacionTecnico
            modelBuilder.Entity<AsignacionTecnico>()
                .HasKey(at => at.AsignacionId);
            modelBuilder.Entity<AsignacionTecnico>()
                .HasOne(at => at.Usuario)
                .WithMany(u => u.AsignacionesTecnico)
                .HasForeignKey(at => at.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<AsignacionTecnico>()
                .HasOne(at => at.Tecnico)
                .WithMany(t => t.AsignacionesTecnico)
                .HasForeignKey(at => at.TecnicoId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<AsignacionTecnico>()
                .HasOne(at => at.Servicio)
                .WithMany(s => s.AsignacionesTecnicos)
                .HasForeignKey(at => at.ServicioId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
