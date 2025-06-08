﻿using ConcesionariaBackend.Models;
using ConcesionariaBackend.Data;
using Microsoft.EntityFrameworkCore;

public class ClienteRepository : IClienteRepository
{
    private readonly AppDbContext _context;

    public ClienteRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Cliente>> GetAllAsync()
    {
        return await _context.Clientes.ToListAsync();
    }

    public async Task<Cliente?> GetByIdAsync(int id)
    {
        return await _context.Clientes.FindAsync(id);
    }

    // Método que incluye historial
    public async Task<Cliente?> GetByIdWithHistorialAsync(int id)
    {
        return await _context.Clientes
            .Include(c => c.InformesHistoricos)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Cliente> CreateAsync(Cliente cliente)
    {
        _context.Clientes.Add(cliente);
        await _context.SaveChangesAsync();
        return cliente;
    }

    public async Task UpdateAsync(Cliente cliente)
    {
        _context.Entry(cliente).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var cliente = await _context.Clientes.FindAsync(id);
        if (cliente == null) return false;

        _context.Clientes.Remove(cliente);
        await _context.SaveChangesAsync();
        return true;
    }
}