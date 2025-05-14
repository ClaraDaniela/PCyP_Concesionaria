using Microsoft.EntityFrameworkCore;
using ConcesionariaBackend.Models;

namespace ConcesionariaBackend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<ServicioPostVenta> ServiciosPostVenta { get; set; }
        public DbSet<Factura> Facturas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("Cliente");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("idCliente");
                entity.Property(e => e.Nombre).HasColumnName("nombre");
                entity.Property(e => e.Apellido).HasColumnName("apellido");
                entity.Property(e => e.Email).HasColumnName("email");
                entity.Property(e => e.Telefono).HasColumnName("telefono");
                entity.Property(e => e.DNI).HasColumnName("DNI");
            });

            modelBuilder.Entity<Vehiculo>(entity =>
            {
                entity.ToTable("Vehiculo");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("idVehiculo");
                entity.Property(e => e.Marca).HasColumnName("marca");
                entity.Property(e => e.Modelo).HasColumnName("modelo");
                entity.Property(e => e.Anio).HasColumnName("anio");
                entity.Property(e => e.Precio).HasColumnName("precio");
                entity.Property(e => e.Stock).HasColumnName("stock");
                entity.Property(e => e.Color).HasColumnName("color");
            });

            modelBuilder.Entity<Venta>(entity =>
            {
                entity.ToTable("Venta");
                entity.HasKey(e => e.IdVenta);
                entity.Property(e => e.IdVenta).HasColumnName("idVenta");
                entity.Property(e => e.Fecha).HasColumnName("fecha");
                entity.Property(e => e.Total).HasColumnName("total");
                entity.Property(e => e.MetodoPago).HasColumnName("metodoPago");

                entity.Property(e => e.ClienteId).HasColumnName("Cliente_idCliente");
                entity.Property(e => e.VehiculoId).HasColumnName("Vehiculo_idVehiculo");

                entity.HasOne(e => e.Cliente)
                      .WithMany()
                      .HasForeignKey(e => e.ClienteId);

                entity.HasOne(e => e.Vehiculo)
                      .WithMany()
                      .HasForeignKey(e => e.VehiculoId);
            });

            modelBuilder.Entity<ServicioPostVenta>(entity =>
            {
                entity.ToTable("ServicioPosventa");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("idServicioPosventa");
                entity.Property(e => e.TipoServicio).HasColumnName("tipoServicio");
                entity.Property(e => e.Fecha).HasColumnName("fecha");
                entity.Property(e => e.Estado).HasColumnName("estado");
                entity.Property(e => e.Descripcion).HasColumnName("descripcion");

                entity.Property(e => e.ClienteId).HasColumnName("idCliente");
                entity.Property(e => e.VehiculoId).HasColumnName("idVehiculo");

                entity.HasOne(e => e.Cliente)
                      .WithMany()
                      .HasForeignKey(e => e.ClienteId);

                entity.HasOne(e => e.Vehiculo)
                      .WithMany()
                      .HasForeignKey(e => e.VehiculoId);
            });

            modelBuilder.Entity<Factura>(entity =>
            {
                entity.ToTable("Factura");
                entity.HasKey(e => e.IdFactura);
                entity.Property(e => e.IdFactura).HasColumnName("idFactura");
                entity.Property(e => e.NumeroFactura).HasColumnName("numeroFactura");
                entity.Property(e => e.Fecha).HasColumnName("fecha");
                entity.Property(e => e.Total).HasColumnName("total");
                entity.Property(e => e.TipoFactura).HasColumnName("tipoFactura");
                entity.Property(e => e.CuitCliente).HasColumnName("cuitCliente");
                entity.Property(e => e.RazonSocialCliente).HasColumnName("razonSocialCliente");

                entity.Property(e => e.VentaId).HasColumnName("Venta_idVenta");

                entity.HasOne(e => e.Venta)
                      .WithMany()
                      .HasForeignKey(e => e.VentaId);
            });
        }
    }
}


