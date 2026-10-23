using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using Patrimonio.Context;
using Patrimonio.Models;
using Patrimonio.Models.ValueObjects;

namespace Patrimonio.Repositories.Implementations;

public class CadastroRepository : ICadastroRepository
{
    private readonly OracleContext _context;
    private readonly string _schema;

    public CadastroRepository(OracleContext context)
    {
        _context = context;
        _schema = _context.ObterSchema();
    }   

    public async Task<Cadastro?> BuscaItemNoCadastroAsync(CadastroFiltro filtro, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(filtro);

        return await _context.Cadastro
            .AsNoTracking()
            .FirstOrDefaultAsync(c => 
                c.EmpresaId == filtro.Empresa &&
                c.NumeroPatrimonial == filtro.NumeroPatrimonial && 
                c.TipoIncorporacaoId == filtro.TipoIncorporacao && 
                c.Incorporacao == filtro.Incorporacao, cancellationToken);
    }

    public async Task<bool> ValidaPlaquetaNoCadastroAsync(ValidaCadastroFiltro filtro, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(filtro);

        return await _context.Cadastro
            .AsNoTracking()
            .AnyAsync(c => 
                c.EmpresaId == filtro.Empresa &&
                c.NumeroPatrimonial == filtro.NumeroPatrimonial &&
                c.EmpresaFilialId != "281",
                cancellationToken
            );
    }
}
