using Microsoft.EntityFrameworkCore;
using ControlAccesoEventos.Domain.Entities;

namespace ControlAccesEvento.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Invitado> Invitados { get; set; }
        public DbSet<Entrada> Entradas { get; set; }
        public DbSet<Validacion> Validaciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Evento>(eb =>
            {
                eb.HasKey(e => e.Id);
                eb.Property(e => e.Nombre).IsRequired();
                eb.Property(e => e.Fecha).IsRequired();
                eb.Property(e => e.Lugar).IsRequired();
                eb.HasMany(typeof(Entrada), "Entradas").WithOne("Evento").HasForeignKey("EventoId").OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Invitado>(ib =>
            {
                ib.HasKey(i => i.Id);
                ib.Property(i => i.DocumentoIdentidad).IsRequired(false);
                ib.HasMany(typeof(Entrada), "Entradas").WithOne("Invitado").HasForeignKey("InvitadoId").OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Entrada>(enb =>
            {
                enb.HasKey(e => e.Id);
                enb.Property(e => e.CodigoAcceso).IsRequired();
                enb.Property(e => e.FechaEmision).IsRequired();
                enb.HasMany(typeof(Validacion), "Validaciones").WithOne("Entrada").HasForeignKey("EntradaId").OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Validacion>(vb =>
            {
                vb.HasKey(v => v.Id);
                vb.Property(v => v.Resultado).IsRequired();
                vb.Property(v => v.FechaHora).IsRequired();
            });
        }
    }
}
