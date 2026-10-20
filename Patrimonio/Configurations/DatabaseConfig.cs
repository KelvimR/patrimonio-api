using Microsoft.EntityFrameworkCore;
using Patrimonio.Context;

namespace Patrimonio.Configurations;

public static class DatabaseConfig
{
    public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["ConnectionStrings:Database"];
        if(String.IsNullOrEmpty(connectionString))
            throw new ArgumentNullException("Conexão com banco de dados não configurada.");

        services.AddDbContext<OracleContext>(options => 
            options.UseOracle(connectionString));
        
        return services;
    }
}
