using Microsoft.EntityFrameworkCore;
using PrestamoApp.Models;

namespace PrestamoApp.Data
{
    public class PrestamoDbContext : DbContext
    {
        public PrestamoDbContext(DbContextOptions<PrestamoDbContext> options) : base(options)
        {
        }

        public DbSet<Cliente> Cliente { get; set; }
        //public DbSet<Moneda> Monedas { get; set; }
        //public DbSet<Prestamo> Prestamos { get; set; }
       // public DbSet<PrestamoDetalle> PrestamoDetalles { get; set; }
        public DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configuraciones adicionales si las necesitas
            // Mapear explícitamente la entidad Usuario a la tabla Usuario
            modelBuilder.Entity<Usuario>().ToTable("Usuario");
        }
    }
}

