using ConcesionariaBackend.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Factura> Facturas { get; set; }
    public DbSet<Venta> Ventas { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Vehiculo> Vehiculos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Factura>()
            .HasKey(f => f.IdFactura);

        modelBuilder.Entity<Factura>()
            .HasOne(f => f.Venta)
            .WithMany(v => v.Facturas)
            .HasForeignKey(f => f.VentaId);

        modelBuilder.Entity<Venta>()
            .HasKey(v => v.IdVenta);

        modelBuilder.Entity<Venta>()
            .HasOne(v => v.Cliente)
            .WithMany()
            .HasForeignKey(v => v.ClienteId);

        modelBuilder.Entity<Venta>()
            .HasOne(v => v.Vehiculo)
            .WithMany()
            .HasForeignKey(v => v.VehiculoId);
    }
}
