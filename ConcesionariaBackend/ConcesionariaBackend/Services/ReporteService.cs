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

            var tareas = clientes.Select(async cliente =>
            {
                // Consultas paralelas por cliente
                var ventasTask = _context.Ventas
                    .Where(v => v.ClienteId == cliente.Id)
                    .ToListAsync();

                var serviciosTask = _context.ServiciosPostVenta
                    .Where(s => s.ClienteId == cliente.Id)
                    .ToListAsync();

                await Task.WhenAll(ventasTask, serviciosTask);

                var ventasCliente = await ventasTask;
                var serviciosCliente = await serviciosTask;

                return new ReporteDTO
                {
                    ClienteNombre = $"{cliente.Nombre} {cliente.Apellido}",
                    CantidadVentas = ventasCliente.Count,
                    MontoTotalVentas = ventasCliente.Sum(v => v.Total),
                    CantidadServicios = serviciosCliente.Count,
                    MontoTotalServicios = serviciosCliente.Sum(s => s.Precio),
                    FechaGeneracion = DateTime.Now
                };
            });

            var reportes = await Task.WhenAll(tareas);

            return reportes.ToList();
        }

    }
}
