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
        public async Task<List<VehiculoDTO>> DisponibilidadVehiculosAsync()
        {
            var vehiculos = await _repository.GetAllAsync();
            //Implementacion de paralelismno para mejorar el rendimiento en la obtencion de 
            // vehiculos disponibles, para ello se uso parallel LINQ
            var disponibles = vehiculos
                .AsParallel() //coleccion a multihilos
                .WithDegreeOfParallelism(Environment.ProcessorCount)
                .Where(v => v.Stock > 0)// filtro de vehiculos disponibles
                .ToList(); //se ejecuta una consulta y devuelve una lista

            return _mapper.Map<List<VehiculoDTO>>(disponibles);
        }
    }
    }

