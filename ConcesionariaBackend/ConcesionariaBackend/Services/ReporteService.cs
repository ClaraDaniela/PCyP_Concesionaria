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

        public async Task<List<ReporteVentasPorClienteDTO>> ObtenerVentasPorClienteAsync()
        {
            var resultado = await (from venta in _context.Ventas
                                   join cliente in _context.Clientes on venta.ClienteId equals cliente.Id
                                   group venta by new { cliente.Nombre, cliente.Apellido } into g
                                   select new ReporteVentasPorClienteDTO
                                   {
                                       Nombre = g.Key.Nombre,
                                       Apellido = g.Key.Apellido,
                                       CantidadVentas = g.Count(),
                                       MontoTotal = g.Sum(v => v.Total)
                                   }).ToListAsync();

            return resultado;
        }
    }
}