using SleepSync.Domain.Entities;

namespace SleepSync.Application.Interfaces;

public interface ISleepRepository {
    Task<IEnumerable<SleepGlobal>> GetAllAsync();
    Task<SleepGlobal?> GetByIdAsync(int id);
    Task<SleepGlobal> AddAsync(SleepGlobal sleep);
    Task UpdateAsync(SleepGlobal sleep);
    Task<bool> DeleteAsync(int id);
}