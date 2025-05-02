using ConcesionariaBackend.DTOs;
using ConcesionariaBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConcesionariaBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportesController : ControllerBase
    {
        private readonly ReporteService _reporteService;

        public ReportesController(ReporteService reporteService)
        {
            _reporteService = reporteService;
        }

        [HttpGet("ventas-por-cliente")]
        public async Task<ActionResult<List<ReporteVentasPorClienteDTO>>> GetVentasPorCliente()
        {
            var reporte = await _reporteService.ObtenerVentasPorClienteAsync();
            return Ok(reporte);
        }
    }
}
