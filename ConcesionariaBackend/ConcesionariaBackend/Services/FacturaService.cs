using AutoMapper;
using ConcesionariaBackend.DTOs;
using ConcesionariaBackend.Models;
using ConcesionariaBackend.Repositories;

namespace ConcesionariaBackend.Services
{
    public class FacturaService
    {
        private readonly IFacturaRepository _facturaRepository;
        private readonly IMapper _mapper;

        public FacturaService(IFacturaRepository facturaRepository, IMapper mapper)
        {
            _facturaRepository = facturaRepository;
            _mapper = mapper;
        }

        public async Task<List<FacturaDTO>> GetAllAsync()
        {
            var facturas = await _facturaRepository.GetAllAsync();
            return _mapper.Map<List<FacturaDTO>>(facturas);
        }

        public async Task<FacturaDTO?> GetByIdAsync(int id)
        {
            var factura = await _facturaRepository.GetByIdAsync(id);
            return factura == null ? null : _mapper.Map<FacturaDTO>(factura);
        }

        public async Task<FacturaDTO> CreateAsync(FacturaDTO dto)
        {
            var factura = _mapper.Map<Factura>(dto);
            factura.Fecha = DateTime.Now;
            var created = await _facturaRepository.CreateAsync(factura);
            return _mapper.Map<FacturaDTO>(created);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _facturaRepository.DeleteAsync(id);
        }
    }
}
