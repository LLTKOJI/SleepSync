using SleepSync.Domain.Entities;

namespace SleepSync.Application.Interfaces;

public interface ISleepRepository {
    Task<IEnumerable<SleepGlobal>> GetAllAsync();
    Task<SleepGlobal> AddAsync(SleepGlobal sleep); 
}