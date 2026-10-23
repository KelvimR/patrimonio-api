using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using Patrimonio.Context;
using Patrimonio.Models;
using Patrimonio.Models.ValueObjects;

namespace Patrimonio.Repositories.Implementations;

public class CentroCustoRepository : ICentroCustoRepository
{
    private readonly OracleContext _context;
    private readonly string _schema;

    public CentroCustoRepository(OracleContext context)
    {
        _context = context;
        _schema = _context.ObterSchema();
    }

    public async Task<List<CentroCusto>> ObterCentroCustoAsync(ObterCentroCustoFiltro filtro, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(filtro);

        return await (
                from ia in _context.InventarioAuditoria.AsNoTracking()
                join cc in _context.CentroCusto.AsNoTracking()
                    on new { ia.EmpresaId, ia.EmpresaFilialId, ia.CentroCustoId }
                    equals new { cc.EmpresaId, cc.EmpresaFilialId, cc.CentroCustoId}
                where ia.EmpresaId == filtro.Empresa
                   && ia.EmpresaFilialId == filtro.EmpresaFilial
                   && ia.InventarioId == filtro.Inventario
                select cc
            )
            .Distinct()
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ValidarCentroCustoAsync(ValidarCentroCustoFiltro filtro, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(filtro);

        return await _context.CentroCusto
            .AsNoTracking()
            .AnyAsync(c =>
                c.EmpresaId == filtro.Empresa &&
                c.EmpresaFilialId == filtro.EmpresaFilial &&
                c.CentroCustoId == filtro.CentroCusto,
                cancellationToken
            );
    }
}
