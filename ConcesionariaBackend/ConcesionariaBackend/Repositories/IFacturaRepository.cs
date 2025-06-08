using ConcesionariaBackend.Models;

namespace ConcesionariaBackend.Repositories.Interfaces
{
    public interface IFacturaRepository
    {
        Task<List<Factura>> GetAllAsync();
        Task<Factura?> GetByIdAsync(int id);
        Task<Factura> CreateAsync(Factura factura);
        Task<bool> DeleteAsync(int id);
    }
}

