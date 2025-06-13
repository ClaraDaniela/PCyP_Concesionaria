using ConcesionariaBackend.Data;
using ConcesionariaBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace ConcesionariaBackend.Repositories
{
    public class VehiculoRepositorySucursalB : IVehiculoRepository
    {
        private readonly AppDbContext _context;

        public VehiculoRepositorySucursalB(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Vehiculo>> GetAllAsync()
        {
            return await _context.Vehiculos.Include(v => v.Sucursal).ToListAsync();
        }

        public async Task<Vehiculo?> GetByIdAsync(int id)
        {
            return await _context.Vehiculos.FindAsync(id);
        }

        public async Task<Vehiculo> AddAsync(Vehiculo entity)
        {
            _context.Vehiculos.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(Vehiculo entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Vehiculos.FindAsync(id);
            if (entity == null) return false;

            _context.Vehiculos.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

