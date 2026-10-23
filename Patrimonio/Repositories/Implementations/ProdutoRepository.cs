using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using Patrimonio.Context;
using Patrimonio.Models;

namespace Patrimonio.Repositories.Implementations;

public class ProdutoRepository : IProdutoRepository
{
    private readonly OracleContext _context;    
    private readonly string _schema;

    public ProdutoRepository(OracleContext context)
    {
        _context = context;
        _schema = _context.ObterSchema();
    }

    public Task<List<Produto>> ObterProdutosAsync(CancellationToken cancellationToken)
    {
        return _context.Produto
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Produto>> ObterProdutosPorDescricaoAsync(string produto, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(produto);

        var descricao = $"%{produto.Trim().ToUpperInvariant()}%";

        return await _context.Produto
            .AsNoTracking()
            .Where(p => p.Descricao != null &&
                EF.Functions.Like(p.Descricao.ToUpper(), descricao))
            .OrderBy(p => p.ProdutoId)
            .ToListAsync(cancellationToken);
    }

    public async Task<Produto?> ObterProdutosPorIdAsync(int produtoId, CancellationToken cancellationToken)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(produtoId);

        var sql = $"""
                SELECT PRODUTOID, DESCRICAO
                FROM {_schema}.PRODUTO
                WHERE PRODUTOID = {produtoId}
                    AND ROWNUM <= 1
                """;

        var produtos = await _context.Produto
            .FromSqlRaw(
                sql,
                new OracleParameter("produtoId", produtoId))
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return produtos.FirstOrDefault();
    }
}
