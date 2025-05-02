using ConcesionariaBackend.DTOs;
using ConcesionariaBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConcesionariaBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FacturaController : ControllerBase
    {
        private readonly FacturaService _facturaService;

        public FacturaController(FacturaService facturaService)
        {
            _facturaService = facturaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _facturaService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var factura = await _facturaService.GetByIdAsync(id);
            if (factura == null) return NotFound();
            return Ok(factura);
        }

        [HttpPost]
        public async Task<IActionResult> Create(FacturaDTO dto)
        {
            var factura = await _facturaService.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = factura.VentaId }, factura);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _facturaService.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}

