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
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<Plan> Planes { get; set; }
        public DbSet<Tecnico> Tecnicos { get; set; }
        public DbSet<Trabajo> Trabajos { get; set; }
        public DbSet<Cita> Citas { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<HistorialUsoPlan> HistorialUsoPlanes { get; set; }
        public DbSet<DispositivoPlanFamiliar> DispositivosPlanFamiliar { get; set; }
        public DbSet<TarjetaGuardada> TarjetasGuardadas { get; set; }
        public DbSet<BitacoraEvento> BitacoraEventos { get; set; }
        public DbSet<AcercaDe> AcercaDe { get; set; }
        public DbSet<PoliticaPrivacidad> PoliticasPrivacidad { get; set; }
        public DbSet<AsignacionTecnico> AsignacionesTecnicos { get; set; }
        public DbSet<CambioContratacion> CambiosContratacion { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Plan_Servicio>().HasKey(ps => new { ps.PlanId, ps.ServicioId });
        }
    }
}
