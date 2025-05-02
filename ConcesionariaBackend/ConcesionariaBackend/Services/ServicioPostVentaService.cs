using AutoMapper;
using ConcesionariaBackend.DTOs;
using ConcesionariaBackend.Models;
using ConcesionariaBackend.Repositories;

namespace ConcesionariaBackend.Services
{
    public class ServicioPostVentaService
    {
        private readonly IServicioPostVentaRepository _repository;
        private readonly IMapper _mapper;

        public ServicioPostVentaService(IServicioPostVentaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // Obtener todos los servicios postventa
        public async Task<IEnumerable<ServicioPostVentaDTO>> GetAllAsync()
        {
            var servicios = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ServicioPostVentaDTO>>(servicios);
        }

        // Obtener un servicio postventa por su ID
        public async Task<ServicioPostVentaDTO> GetByIdAsync(int id)
        {
            var servicio = await _repository.GetByIdAsync(id);
            if (servicio == null) return null;
            return _mapper.Map<ServicioPostVentaDTO>(servicio);
        }

        // Crear un nuevo servicio postventa
        public async Task<ServicioPostVentaDTO> CreateAsync(ServicioPostVentaDTO dto)
        {
            var servicio = _mapper.Map<ServicioPostVenta>(dto);
            var createdServicio = await _repository.CreateAsync(servicio);
            return _mapper.Map<ServicioPostVentaDTO>(createdServicio);
        }

        // Actualizar un servicio postventa
        public async Task<bool> UpdateAsync(ServicioPostVentaDTO dto)
        {
            var servicio = await _repository.GetByIdAsync(dto.Id);
            if (servicio == null) return false;
            _mapper.Map(dto, servicio);
            return await _repository.UpdateAsync(servicio);
        }

        // Eliminar un servicio postventa
        public async Task<bool> DeleteAsync(int id)
        {
            var servicio = await _repository.GetByIdAsync(id);
            if (servicio == null) return false;
            return await _repository.DeleteAsync(servicio);
        }

        internal async Task AddAsync(ServicioPostVentaDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
