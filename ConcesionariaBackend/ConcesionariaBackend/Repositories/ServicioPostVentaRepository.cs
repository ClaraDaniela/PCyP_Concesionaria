using ConcesionariaBackend.Data;
using ConcesionariaBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace ConcesionariaBackend.Repositories
{
    public class ServicioPostVentaRepository : IServicioPostVentaRepository
    {
        private readonly AppDbContext _context;

        public ServicioPostVentaRepository(AppDbContext context)
        {
            _context = context;
        }

        // Obtener todos los servicios post venta
        public async Task<IEnumerable<ServicioPostVenta>> GetAllAsync()
        {
            return await _context.ServiciosPostVenta.ToListAsync();
        }

        // Obtener un servicio post venta por su ID
        public async Task<ServicioPostVenta> GetByIdAsync(int id)
        {
            return await _context.ServiciosPostVenta.FindAsync(id);
        }

        // Crear un nuevo servicio post venta
        public async Task<ServicioPostVenta> CreateAsync(ServicioPostVenta entity)
        {
            await _context.ServiciosPostVenta.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        // Actualizar un servicio post venta
        public async Task<bool> UpdateAsync(ServicioPostVenta entity)
        {
            _context.ServiciosPostVenta.Update(entity);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        // Eliminar un servicio post venta por su ID
        public async Task<bool> DeleteAsync(ServicioPostVenta entity)
        {
            _context.ServiciosPostVenta.Remove(entity);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(ServicioPostVenta entity)
        {
            throw new NotImplementedException();
        }
    }
}
