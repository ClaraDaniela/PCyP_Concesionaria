using ConcesionariaBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace ConcesionariaBackend.Repositories
{
    public interface IFacturaRepository
    {
        Task<List<Factura>> GetAllAsync();
        Task<Factura?> GetByIdAsync(int id);
        Task<Factura> CreateAsync(Factura factura);
        Task<bool> DeleteAsync(int id);
    }

    public class FacturaRepository : IFacturaRepository
    {
        private readonly ApplicationDbContext _context;

        public FacturaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Factura>> GetAllAsync()
        {
            return await _context.Facturas
                .Include(f => f.Venta)
                    .ThenInclude(v => v.Cliente)
                .Include(f => f.Venta)
                    .ThenInclude(v => v.Vehiculo)
                .ToListAsync();
        }

        public async Task<Factura?> GetByIdAsync(int id)
        {
            return await _context.Facturas
                .Include(f => f.Venta)
                    .ThenInclude(v => v.Cliente)
                .Include(f => f.Venta)
                    .ThenInclude(v => v.Vehiculo)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<Factura> CreateAsync(Factura factura)
        {
            _context.Facturas.Add(factura);
            await _context.SaveChangesAsync();
            return factura;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var factura = await _context.Facturas.FindAsync(id);
            if (factura == null)
                return false;

            _context.Facturas.Remove(factura);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

