using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ConcesionariaBackend.Services;
using ConcesionariaBackend.DTOs;

namespace ConcesionariaBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicioPostVentaController: ControllerBase
    {
        private readonly ServicioPostVentaService _service;
        private readonly IValidator<ServicioPostVentaDTO> _validator;

        public ServicioPostVentaController(ServicioPostVentaService service, IValidator<ServicioPostVentaDTO> validator)
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
        public async Task<IActionResult> Create([FromBody] ServicioPostVentaDTO dto)
        {
            var result = await _validator.ValidateAsync(dto);
            if (!result.IsValid) return BadRequest(result.Errors);
            await _service.AddAsync(dto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ServicioPostVentaDTO dto)
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
