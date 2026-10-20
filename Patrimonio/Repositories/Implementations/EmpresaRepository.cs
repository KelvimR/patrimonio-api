using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Patrimonio.Context;
using Patrimonio.Data.DTO;
using Patrimonio.Models;

namespace Patrimonio.Repositories.Implementations;

public class EmpresaRepository : IEmpresaRepository
{
    private readonly OracleContext _context;
   
    public EmpresaRepository(OracleContext context) => _context = context;
    
    public Task<Empresa> CriarEmpresaAsync(Empresa empresa, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<List<Empresa>> ObterEmpresasAsync(CancellationToken cancellationToken)
    {
        return _context.Empresas
            .AsNoTracking()            
            .ToListAsync(cancellationToken);
    }

    public Task<Empresa?> ObterEmpresaPorIdAsync(string empresaId, CancellationToken cancellationToken)
    {
        return _context.Empresas
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.EmpresaId == empresaId, cancellationToken);
    }       
    
    public Task<Empresa> AtualizarEmpresaAsync(Empresa empresa, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task DeletarEmpresa(string empresaId, CancellationToken cancellationToken    )
    {
        _context.Remove(empresaId);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
