using Microsoft.EntityFrameworkCore;
using Patrimonio.Models;
using System.Text.RegularExpressions;

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

        if (!Regex.IsMatch(_schema, @"^[A-Z][A-Z0-9_$#]*$"))
            throw new InvalidOperationException("Schema inválido.");
    }

    public string ObterSchema()
    {
        return _schema.ToUpperInvariant();
    }

    public DbSet<Empresa> Empresas { get; set; }
    public DbSet<Produto> Produto { get; set; }
    public DbSet<Cadastro> Cadastro { get; set; }
    public DbSet<CentroCusto> CentroCusto { get; set; }
    public DbSet<InventarioAuditoria> InventarioAuditoria { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema(_schema);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OracleContext).Assembly);
    }
}
