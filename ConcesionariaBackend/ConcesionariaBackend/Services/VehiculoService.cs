using AutoMapper;
using ConcesionariaBackend.DTOs;
using ConcesionariaBackend.Models;
using ConcesionariaBackend.Repositories;

namespace ConcesionariaBackend.Services
{
    public class VehiculoService
    {
        private readonly IVehiculoRepository _repository;
        private readonly IMapper _mapper;

        public VehiculoService(IVehiculoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<VehiculoDTO>> GetAllAsync()
        {
            var vehiculos = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<VehiculoDTO>>(vehiculos);
        }

        public async Task<VehiculoDTO?> GetByIdAsync(int id)
        {
            var vehiculo = await _repository.GetByIdAsync(id);
            return vehiculo == null ? null : _mapper.Map<VehiculoDTO>(vehiculo);
        }

        public async Task<VehiculoDTO> AddAsync(VehiculoDTO dto)
        {
            var entity = _mapper.Map<Vehiculo>(dto);
            var result = await _repository.AddAsync(entity);
            return _mapper.Map<VehiculoDTO>(result);
        }

        public async Task UpdateAsync(int id, VehiculoDTO dto)
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

        public async Task<List<VehiculoDTO>> SimularDisponibilidadVehiculosAsync(string? marcaFiltro = null)
        {
            var concesionarias = new[] { "Sucursal A", "Sucursal B", "Sucursal C" };

            var tasks = concesionarias.Select(async nombre =>
            {
                var vehiculos = await _repository.GetAllAsync();
                var dto = _mapper.Map<List<VehiculoDTO>>(vehiculos);

                return dto.AsParallel()
                          .WithDegreeOfParallelism(Environment.ProcessorCount)
                          .Where(v => string.IsNullOrEmpty(marcaFiltro) || (!string.IsNullOrEmpty(v.Marca) && v.Marca.Contains(marcaFiltro, StringComparison.OrdinalIgnoreCase)))
                          .ToList();
            });

            var resultados = await Task.WhenAll(tasks);
            return resultados.SelectMany(r => r).ToList(); 
        }
    }
}

