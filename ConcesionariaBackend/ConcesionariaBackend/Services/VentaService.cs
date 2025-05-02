using AutoMapper;
using ConcesionariaBackend.DTOs;
using ConcesionariaBackend.Models;
using ConcesionariaBackend.Repositories;

namespace ConcesionariaBackend.Services
{
    public class VentaService
    {
        private readonly IVentaRepository _repository;
        private readonly IMapper _mapper;

        public VentaService(IVentaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<VentaDTO>> GetAllAsync()
        {
            var ventas = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<VentaDTO>>(ventas);
        }

        public async Task<VentaDTO?> GetByIdAsync(int id)
        {
            var venta = await _repository.GetByIdAsync(id);
            return venta == null ? null : _mapper.Map<VentaDTO>(venta);
        }

        public async Task<VentaDTO> AddAsync(VentaDTO dto)
        {
            var entity = _mapper.Map<Venta>(dto);
            var result = await _repository.AddAsync(entity);
            return _mapper.Map<VentaDTO>(result);
        }

        public async Task UpdateAsync(int id, VentaDTO dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing != null)
            {
                _mapper.Map(dto, existing);
                await _repository.UpdateAsync(existing);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        internal async Task UpdateAsync(VentaDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}

