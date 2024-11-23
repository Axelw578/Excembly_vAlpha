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
        public DbSet<Contratacion> Contratacion{ get; set; }
        public DbSet<ServicioAdicionalContratado> ServicioAdicionalContratado { get; set; }
        public DbSet<ServicioAdicional> ServicioAdicional { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Configuración de relaciones en Contratacion
            modelBuilder.Entity<Contratacion>()
                .HasOne(c => c.Usuario)
                .WithMany(u => u.Contrataciones)
                .HasForeignKey(c => c.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Contratacion>()
                .HasOne(c => c.Plan)
                .WithMany(p => p.Contrataciones)
                .HasForeignKey(c => c.PlanId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Contratacion>()
                .HasOne(c => c.Servicio)
                .WithMany(s => s.Contrataciones)
                .HasForeignKey(c => c.ServicioId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Contratacion>()
                .HasOne(c => c.Cita)
                .WithOne(c => c.Contratacion)
                .HasForeignKey<Contratacion>(c => c.CitaId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<PlanServicio>()
    .HasKey(ps => new { ps.PlanId, ps.ServicioId }); 

            modelBuilder.Entity<PlanServicio>()
                .HasOne(ps => ps.Plan)
                .WithMany(p => p.PlanServicios)
                .HasForeignKey(ps => ps.PlanId);

            modelBuilder.Entity<PlanServicio>()
                .HasOne(ps => ps.Servicio)
                .WithMany(s => s.PlanServicios)
                .HasForeignKey(ps => ps.ServicioId);



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

            // Configuración de ServicioAdicional
            modelBuilder.Entity<ServicioAdicional>()
                .ToTable("serviciosadicionales"); // Nombre de la tabla

            modelBuilder.Entity<ServicioAdicional>()
                .HasKey(sa => new { sa.PlanId, sa.ServicioId }); // Llave compuesta

            modelBuilder.Entity<ServicioAdicional>()
                .HasOne(sa => sa.Plan)
                .WithMany(p => p.ServiciosAdicionales)
                .HasForeignKey(sa => sa.PlanId)
                .OnDelete(DeleteBehavior.Restrict); // Relación con Plan

            modelBuilder.Entity<ServicioAdicional>()
                .HasOne(sa => sa.Servicio)
                .WithMany(s => s.ServiciosAdicionales)
                .HasForeignKey(sa => sa.ServicioId)
                .OnDelete(DeleteBehavior.Restrict); // Relación con Servicio




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

            // Configuración del modelo Comentario
            modelBuilder.Entity<Comentario>()
                .HasOne(c => c.Contratacion)
                .WithMany(ct => ct.Comentarios)  
                .HasForeignKey(c => c.ContratacionId)
                .OnDelete(DeleteBehavior.Restrict);





        }
    }
}
