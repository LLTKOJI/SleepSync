using Microsoft.EntityFrameworkCore;
using SleepSync.Application.Interfaces;
using SleepSync.Domain.Entities;
using SleepSync.Infrastructure.Data;

namespace SleepSync.Infrastructure.Repositories;

// Implementamos la interfaz (La finca obedece a la receta)
public class SleepRepository : ISleepRepository {
    private readonly SleepSyncDbContext _context;
    
    // El constructor recibe el contexto de EF Core
    public SleepRepository(SleepSyncDbContext context) {
        _context = context;
    }

    public async Task<IEnumerable<SleepGlobal>> GetAllAsync() {
        return await _context.SleepGlobales.ToListAsync(); // Consulta real a BD (Magia SQL)
    }
}