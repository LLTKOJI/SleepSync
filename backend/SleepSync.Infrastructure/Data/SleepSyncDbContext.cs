using Microsoft.EntityFrameworkCore;
using SleepSync.Domain.Entities;

namespace SleepSync.Infrastructure.Data;

public class SleepSyncDbContext : DbContext {
    public SleepSyncDbContext(DbContextOptions<SleepSyncDbContext> options) : base(options) { }
    
    public DbSet<SleepGlobal> SleepGlobales { get; set; }
}