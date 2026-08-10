using ControlAccesoEventos.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ControlAccesoEventos.Infrastructure.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Evento> Eventos => Set<Evento>();
        public DbSet<Invitado> Invitados => Set<Invitado>();
        public DbSet<Entrada> Entradas => Set<Entrada>();
        public DbSet<Validacion> Validaciones => Set<Validacion>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Evento>().ToTable("Eventos");
            modelBuilder.Entity<Invitado>().ToTable("Invitados");
            modelBuilder.Entity<Entrada>().ToTable("Entradas");
            modelBuilder.Entity<Validacion>().ToTable("Validaciones");


            modelBuilder.Entity<Entrada>()
                .HasOne(e => e.Evento)
                .WithMany()
                .HasForeignKey(e => e.EventoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Entrada>()
                .HasOne(e => e.Invitado)
                .WithMany()
                .HasForeignKey(e => e.InvitadoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Validacion>()
                .HasOne(v => v.Entrada)
                .WithMany()
                .HasForeignKey(v => v.EntradaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
