using AutoMapper;
using ConcesionariaBackend.DTOs;
using ConcesionariaBackend.Models;
using ConcesionariaBackend.Repositories;
using System.Collections.Concurrent;

namespace ConcesionariaBackend.Services
{
    public class VentaService
    {
        private readonly IVentaRepository _repository;
        private readonly IVehiculoRepository _vehiculoRepository;
        private readonly IMapper _mapper;
        private readonly SemaphoreSlim _semaforo = new(1, 1);


        public VentaService(IVentaRepository ventaRepo, IVehiculoRepository vehiculoRepo, IMapper mapper)
        {
            _repository = ventaRepo;
            _vehiculoRepository = vehiculoRepo;
            _mapper = mapper;
        }
        //Aca implemente DataFlow con concurrentqueue
        public async Task ProcesarVentasConcurrentesAsync(IEnumerable<VentaDTO> ventasPendientes)
        {
            var cola = new ConcurrentQueue<VentaDTO>(ventasPendientes);

            var tasks = new List<Task>();
            for (int i = 0; i < Environment.ProcessorCount; i++) // Cantidad de hilos según CPU
            {   //implemente task.run para ejecutar tareas asincronas en un hilo separadp
                tasks.Add(Task.Run(async () =>
                {
                    while (cola.TryDequeue(out var venta))
                    {
                        await ProcesarVentaSeguraAsync(venta);
                    }
                }));
            }

            await Task.WhenAll(tasks);
        }

        private async Task ProcesarVentaSeguraAsync(VentaDTO venta)
        {
            try
            {
                await _semaforo.WaitAsync();
                var vehiculo = await _vehiculoRepository.GetByIdAsync(venta.VehiculoId);

                if (vehiculo == null || vehiculo.Stock <= 0)
                {
                    Console.WriteLine($"Sin stock para vehículo ID: {venta.VehiculoId}");
                    return;
                }

                vehiculo.Stock--; // Reserva el vehículo
                await _vehiculoRepository.UpdateAsync(vehiculo); // guarda el cambio en BD
            

                // Guardamos la venta
                var ventaEntity = _mapper.Map<Venta>(venta);
                await _repository.AddAsync(ventaEntity);
                Console.WriteLine($"Venta procesada para vehículo ID: {venta.VehiculoId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error procesando venta: {ex.Message}");
            }
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

