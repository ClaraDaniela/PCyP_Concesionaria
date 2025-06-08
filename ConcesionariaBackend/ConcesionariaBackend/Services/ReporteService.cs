using ConcesionariaBackend.Data;
using ConcesionariaBackend.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ConcesionariaBackend.Services
{
    public class ReporteService
    {
        private readonly AppDbContext _context;

        public ReporteService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ReporteDTO>> ObtenerVentasPorClienteAsync()
        {
            var clientes = await _context.Clientes.ToListAsync();

            var reportes = new List<ReporteDTO>();

            foreach (var cliente in clientes)
            {
                var ventasCliente = await _context.Ventas
                    .Where(v => v.ClienteId == cliente.Id)
                    .ToListAsync();

                var serviciosCliente = await _context.ServiciosPostVenta
                    .Where(s => s.ClienteId == cliente.Id)
                    .ToListAsync();

                var reporte = new ReporteDTO
                {
                    ClienteNombre = $"{cliente.Nombre} {cliente.Apellido}",
                    CantidadVentas = ventasCliente.Count,
                    MontoTotalVentas = ventasCliente.Sum(v => v.Total),
                    CantidadServicios = serviciosCliente.Count,
                    MontoTotalServicios = serviciosCliente.Sum(s => s.Precio),
                    FechaGeneracion = DateTime.Now
                };

                reportes.Add(reporte);
            }

            return reportes;
        }
    }
}
