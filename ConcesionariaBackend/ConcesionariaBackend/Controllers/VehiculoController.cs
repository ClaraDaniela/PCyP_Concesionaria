using ConcesionariaBackend.DTOs;
using ConcesionariaBackend.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace ConcesionariaBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehiculoController : ControllerBase
    {
        private readonly VehiculoService _service;
        private readonly IValidator<VehiculoDTO> _validator;

        public VehiculoController(VehiculoService service, IValidator<VehiculoDTO> validator)
        {
            _service = service;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] VehiculoDTO dto)
        {
            var result = await _validator.ValidateAsync(dto);
            if (!result.IsValid) return BadRequest(result.Errors);
            await _service.AddAsync(dto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] VehiculoDTO dto)
        {
            if (id != dto.Id) return BadRequest();
            var result = await _validator.ValidateAsync(dto);
            if (!result.IsValid) return BadRequest(result.Errors);
            await _service.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("disponibilidad")]
        public async Task<IActionResult> Disponibilidad(
            [FromQuery] string? marca = null,
            [FromQuery] string? sucursal = null)
        {
            var vehiculosDisponibles = await _service.DisponibilidadVehiculosAsync();
            return Ok(vehiculosDisponibles);
        }
    }
}
