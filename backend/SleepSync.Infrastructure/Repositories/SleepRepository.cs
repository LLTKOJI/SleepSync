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

    public async Task<SleepGlobal?> GetByIdAsync(int id) {
        return await _context.SleepGlobales.FindAsync(id);
    }

    public async Task<SleepGlobal> AddAsync(SleepGlobal sleep) {
        await _context.SleepGlobales.AddAsync(sleep);
        await _context.SaveChangesAsync();
        return sleep;
    }

    public async Task UpdateAsync(SleepGlobal sleep) {
        _context.SleepGlobales.Update(sleep);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id) {
        var sleep = await _context.SleepGlobales.FindAsync(id);
        if (sleep is null) return false;

        _context.SleepGlobales.Remove(sleep);
        await _context.SaveChangesAsync();
        return true;
    }
}