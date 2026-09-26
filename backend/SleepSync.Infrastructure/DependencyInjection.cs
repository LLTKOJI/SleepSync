using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SleepSync.Application.Interfaces;
using SleepSync.Infrastructure.Data;
using SleepSync.Infrastructure.Repositories;

namespace SleepSync.Infrastructure;

public static class DependencyInjection {
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString) {
        
        // 1. Configuramos el acceso a PostgreSQL
        services.AddDbContext<SleepSyncDbContext>(options =>
            options.UseNpgsql(connectionString));

        // 2. LA INYECCIÓN CLAVE (El contrato firmado por el Gerente):
        // "Cada vez que un Chef (Controlador) pida la Receta (ISleepRepository), 
        // entrégale los datos de la Finca PostgreSQL (SleepRepository)"
        services.AddScoped<ISleepRepository, SleepRepository>();

        return services;
    }
}