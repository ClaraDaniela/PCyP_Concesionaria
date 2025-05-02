using ConcesionariaBackend.DTOs;
using ConcesionariaBackend.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace ConcesionariaBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VentaController: ControllerBase
    {
        private readonly VentaService _service;
        private readonly IValidator<VentaDTO> _validator;

        public VentaController(VentaService service, IValidator<VentaDTO> validator)
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
        public async Task<IActionResult> Create([FromBody] VentaDTO dto)
        {
            var result = await _validator.ValidateAsync(dto);
            if (!result.IsValid) return BadRequest(result.Errors);
            await _service.AddAsync(dto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] VentaDTO dto)
        {
            if (id != dto.Id) return BadRequest();
            var result = await _validator.ValidateAsync(dto);
            if (!result.IsValid) return BadRequest(result.Errors);
            await _service.UpdateAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
