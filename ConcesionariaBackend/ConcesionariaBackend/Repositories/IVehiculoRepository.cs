using ConcesionariaBackend.Models;

namespace ConcesionariaBackend.Repositories
{
    public interface IVehiculoRepository
    {
        Task<IEnumerable<Vehiculo>> GetAllAsync();
        Task<Vehiculo?> GetByIdAsync(int id);
        Task<Vehiculo> AddAsync(Vehiculo entity);
        Task UpdateAsync(Vehiculo entity);
        Task<bool> DeleteAsync(int id);
    }
}

