using Microsoft.AspNetCore.Mvc;
using ConcesionariaBackend.Services;
using AutoMapper;
using ConcesionariaBackend.DTOs;
using ConcesionariaBackend.Models;

namespace ConcesionariaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController: ControllerBase
    {
        private readonly ClienteService _clienteService;
        private readonly IMapper _mapper;

        public ClienteController(ClienteService clienteService, IMapper mapper)
        {
            _clienteService = clienteService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteDTO>>> GetAll()
        {
            var clientes = await _clienteService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<ClienteDTO>>(clientes));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteDTO>> GetById(int id)
        {
            var cliente = await _clienteService.GetByIdAsync(id);
            if (cliente == null) return NotFound();
            return Ok(_mapper.Map<ClienteDTO>(cliente));
        }

        [HttpPost]
        public async Task<ActionResult<ClienteDTO>> Create(ClienteDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var entity = _mapper.Map<Cliente>(dto);
            var nuevo = await _clienteService.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = nuevo.Id }, _mapper.Map<ClienteDTO>(nuevo));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ClienteDTO>> Update(int id, ClienteDTO dto)
        {
            var existente = await _clienteService.GetByIdAsync(id);
            if (existente == null) return NotFound();
            _mapper.Map(dto, existente);
            await _clienteService.UpdateAsync(existente);
            return Ok(_mapper.Map<ClienteDTO>(existente));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _clienteService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        [HttpGet("{id}/historial")]
        public async Task<ActionResult<ClienteConHistorialDTO>> GetClienteConHistorial(int id)
        {
            var clienteHistorial = await _clienteService.GetClienteConHistorialAsync(id);
            if (clienteHistorial == null) return NotFound();
            return Ok(clienteHistorial);
        }
    }
}
