using ConcesionariaBackend.Models;

namespace ConcesionariaBackend.Repositories
{
    public interface IVentaRepository
    {
        Task<IEnumerable<Venta>> GetAllAsync();
        Task<Venta?> GetByIdAsync(int id);
        Task<Venta> AddAsync(Venta entity);
        Task UpdateAsync(Venta entity);
        Task<bool> DeleteAsync(int id);
    }
}

