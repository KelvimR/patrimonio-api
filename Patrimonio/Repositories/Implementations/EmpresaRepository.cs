using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Patrimonio.Context;
using Patrimonio.Data.DTO;
using Patrimonio.Models;
using Patrimonio.Models.ValueObjects;
using System.Runtime.ConstrainedExecution;

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

    public async Task<Empresa?> ObterEmpresaPorIdAsync(string empresaId, CancellationToken cancellationToken)
    {

        var empresas = await _context.Empresas
            .FromSqlInterpolated($"""
                SELECT EMPRESAID, RAZAOSOCIAL, CGC, ENDERECO, TELEFONE, RAMAL, FAX, CIDADE, ESTADO, CEP, 
                    PLACACORPOREA, PLACANAOCORPOREA, CALCULADEPRECIACAO, CALCULADEPRECIACAODOLLAR, DEPRECIAMESAQUISICAO, 
                    DEPRECIAMESAQUISICAODOLLAR, COMPETENCIAINICIAL, COMPETENCIAFINAL, TIPO_CALCULO, FILIAL_CENTRALIZADORA
                FROM EMPRESAS
                WHERE EMPRESAID = {empresaId} 
                """)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

        return empresas.FirstOrDefault();
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
