using ConcesionariaBackend.Data;
using ConcesionariaBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace ConcesionariaBackend.Repositories
{
    public class VentaRepository : IVentaRepository
    {
        private readonly AppDbContext _context;

        public VentaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Venta>> GetAllAsync()
        {
            return await _context.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.Vehiculo)
                .ToListAsync();
        }

        public async Task<Venta?> GetByIdAsync(int id)
        {
            return await _context.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.Vehiculo)
                .FirstOrDefaultAsync(v => v.IdVenta == id);
        }

        public async Task<Venta> AddAsync(Venta entity)
        {
            _context.Ventas.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(Venta entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Ventas.FindAsync(id);
            if (entity == null) return false;

            _context.Ventas.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

