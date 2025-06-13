using AutoMapper;
using ConcesionariaBackend.DTOs;
using ConcesionariaBackend.Models;
using ConcesionariaBackend.Repositories;

namespace ConcesionariaBackend.Services
{
    public class ClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;

        public ClienteService(IClienteRepository clienteRepository, IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Cliente>> GetAllAsync()
        {
            return await _clienteRepository.GetAllAsync();
        }

        public async Task<Cliente?> GetByIdAsync(int id)
        {
            return await _clienteRepository.GetByIdAsync(id);
        }

        // Nuevo método que trae cliente con historial
        public async Task<ClienteConHistorialDTO?> GetClienteConHistorialAsync(int id)
        {
            var cliente = await _clienteRepository.GetByIdWithHistorialAsync(id);
            if (cliente == null) return null;

            return _mapper.Map<ClienteConHistorialDTO>(cliente);
        }


        public async Task<Cliente> CreateAsync(Cliente cliente)
        {
            return await _clienteRepository.CreateAsync(cliente);
        }

        public async Task UpdateAsync(Cliente cliente)
        {
            await _clienteRepository.UpdateAsync(cliente);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _clienteRepository.DeleteAsync(id);
        }
    }
}
