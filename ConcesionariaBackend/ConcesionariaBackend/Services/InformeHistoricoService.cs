using ConcesionariaBackend.Data;
using ConcesionariaBackend.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;

namespace ConcesionariaBackend.Services
{
    public class InformeHistoricoService
    {
        private readonly AppDbContext _context;

        public InformeHistoricoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task GenerarYGuardarInformesAsync()
        {
            var clientes = await _context.Clientes.AsNoTracking().ToListAsync();
            var fecha = DateTime.Now;
            var informes = new ConcurrentBag<InformeHistorico>();

            var ventas = await _context.Ventas.AsNoTracking().ToListAsync();
            var servicios = await _context.ServiciosPostVenta.AsNoTracking().ToListAsync();

            Parallel.ForEach(clientes, cliente =>
            {
                var ventasCliente = ventas.Where(v => v.ClienteId == cliente.Id).ToList();
                var serviciosCliente = servicios.Where(s => s.ClienteId == cliente.Id).ToList();

                if (ventasCliente.Any())
                {
                    informes.Add(new InformeHistorico
                    {
                        ClienteId = cliente.Id,
                        Tipo = "Venta",
                        MontoTotal = ventasCliente.Sum(v => v.Total),
                        FechaGeneracion = fecha
                    });
                }

                if (serviciosCliente.Any())
                {
                    informes.Add(new InformeHistorico
                    {
                        ClienteId = cliente.Id,
                        Tipo = "Servicio",
                        MontoTotal = serviciosCliente.Sum(s => s.Precio),
                        FechaGeneracion = fecha
                    });
                }
            });

        }
    }
}
