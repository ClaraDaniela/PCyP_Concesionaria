using ConcesionariaBackend.Models;

namespace ConcesionariaBackend.Repositories
{
    public interface IServicioPostVentaRepository
    {
        Task<IEnumerable<ServicioPostVenta>> GetAllAsync();
        Task<ServicioPostVenta> GetByIdAsync(int id);
        Task<ServicioPostVenta> CreateAsync(ServicioPostVenta entity);
        Task<bool> UpdateAsync(ServicioPostVenta entity);
        Task<bool> DeleteAsync(ServicioPostVenta entity);
        Task DeleteAsync(int id);
        Task AddAsync(ServicioPostVenta entity);
    }
}

