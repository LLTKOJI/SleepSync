using Microsoft.EntityFrameworkCore;
using SleepSync.Application.Interfaces;
using SleepSync.Domain.Entities;
using SleepSync.Infrastructure.Data;

namespace SleepSync.Infrastructure.Repositories;

public class SleepRepository : ISleepRepository {
    private readonly SleepSyncDbContext _context;
    
    public SleepRepository(SleepSyncDbContext context) {
        _context = context;
    }

    public async Task<IEnumerable<SleepGlobal>> GetAllAsync() {
        return await _context.SleepGlobales.ToListAsync();
    }

    public async Task<SleepGlobal> AddAsync(SleepGlobal sleep) {
        await _context.SleepGlobales.AddAsync(sleep);
        
        
        await _context.SaveChangesAsync(); 
        
        return sleep;
    }
}