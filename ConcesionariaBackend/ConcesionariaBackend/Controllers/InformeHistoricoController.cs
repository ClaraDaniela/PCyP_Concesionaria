using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConcesionariaBackend.Models;
using ConcesionariaBackend.Data;

namespace ConcesionariaBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InformeHistoricoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public InformeHistoricoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/InformeHistorico
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InformeHistorico>>> GetInformes()
        {
            return await _context.InformesHistoricos
                .Include(i => i.Cliente)
                .ToListAsync();
        }

        // POST: api/InformeHistorico/Servicio/{idCliente}
        [HttpPost("Servicio/{idCliente}")]
        public async Task<ActionResult<InformeHistorico>> GenerarInformeServicios(int idCliente)
        {
            var cliente = await _context.Clientes.FindAsync(idCliente);
            if (cliente == null)
                return NotFound("Cliente no encontrado");

            var servicios = await _context.ServiciosPostVenta
                .Where(s => s.ClienteId == idCliente)
                .ToListAsync();

            var montoTotal = servicios.Sum(s => s.Precio);

            var informe = new InformeHistorico
            {
                idCliente = idCliente,
                Cliente = cliente,
                Tipo = "Servicio",
                MontoTotal = montoTotal,
                FechaGeneracion = DateTime.Now
            };

            _context.InformesHistoricos.Add(informe);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInformes), new { id = informe.Id }, informe);
        }

        // POST: api/InformeHistorico/Venta/{idCliente}
        [HttpPost("Venta/{idCliente}")]
        public async Task<ActionResult<InformeHistorico>> GenerarInformeVentas(int idCliente)
        {
            var cliente = await _context.Clientes.FindAsync(idCliente);
            if (cliente == null)
                return NotFound("Cliente no encontrado");

            var ventas = await _context.Ventas
                .Where(v => v.ClienteId == idCliente)
                .ToListAsync();

            var montoTotal = ventas.Sum(v => v.Total);

            var informe = new InformeHistorico
            {
                idCliente = idCliente,
                Cliente = cliente,
                Tipo = "Venta",
                MontoTotal = montoTotal,
                FechaGeneracion = DateTime.Now
            };

            _context.InformesHistoricos.Add(informe);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInformes), new { id = informe.Id }, informe);
        }
    }
}
