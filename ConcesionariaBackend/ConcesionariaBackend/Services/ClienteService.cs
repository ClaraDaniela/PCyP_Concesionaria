using ConcesionariaBackend.DTOs;
using ConcesionariaBackend.Models;
using ConcesionariaBackend.Repositories;

namespace ConcesionariaBackend.Services
{
    public class ClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
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

            // Mapeo manual o con AutoMapper si lo tienes configurado
            var clienteConHistorialDTO = new ClienteConHistorialDTO
            {
                Id = cliente.Id,
                Nombre = cliente.Nombre,
                Apellido = cliente.Apellido,
                DNI = cliente.DNI,
                Email = cliente.Email,
                Telefono = cliente.Telefono,
                Historial = cliente.InformesHistoricos.Select(i => new InformeHistoricoDTO
                {
                    Id = i.Id,
                    Tipo = i.Tipo,
                    MontoTotal = i.MontoTotal,
                    FechaGeneracion = i.FechaGeneracion
                }).ToList()
            };

            return clienteConHistorialDTO;
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
