using ConcesionariaBackend.DTOs;
using ConcesionariaBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConcesionariaBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReporteController : ControllerBase
    {
        private readonly ReporteService _reporteService;

        public ReporteController(ReporteService reporteService)
        {
            _reporteService = reporteService;
        }

        [HttpGet("ventas-por-cliente")]
        public async Task<ActionResult<List<ReporteDTO>>> GetVentasPorCliente()
        {
            var reporte = await _reporteService.ObtenerVentasPorClienteAsync();
            return Ok(reporte);
        }
    }
}
