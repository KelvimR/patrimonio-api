using Microsoft.EntityFrameworkCore;
using Patrimonio.Models;

namespace Patrimonio.Context;

public class OracleContext : DbContext
{
    private readonly string _schema;
    public OracleContext(DbContextOptions<OracleContext> options, IConfiguration configuration) : base(options)
    {
        _schema = configuration["ConnectionStrings:Schema"]
            ?? throw new InvalidOperationException("Schema do banco de dados não configurado.");

        if (string.IsNullOrWhiteSpace(_schema))
            throw new InvalidOperationException("Schema do banco de dados não configurado.");
    }

    public DbSet<Empresa> Empresas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema(_schema);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OracleContext).Assembly);
    }
}
