using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
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

    public async Task<bool> AtualizarEmpresaAsync(Empresa empresa, CancellationToken cancellationToken)
    {               
        await using var transacao = await _context.Database.BeginTransactionAsync(cancellationToken);        
        
        var alterado = await _context.Database.ExecuteSqlInterpolatedAsync($"""
                UPDATE EMPRESAS SET 
                    RAZAOSOCIAL = {empresa.RazaoSocial},
                    ENDERECO = {empresa.Endereco},
                    TELEFONE = {empresa.Telefone},
                    RAMAL = {empresa.Ramal},
                    FAX = {empresa.FAX},
                    CIDADE = {empresa.Cidade},
                    ESTADO = {empresa.Estado},
                    CEP = {empresa.CEP},
                    CGC = {empresa.CGC},
                    FILIAL_CENTRALIZADORA = {empresa.FilialCentralizadora},
                    COMPETENCIAINICIAL = {empresa.CompetenciaInicial},
                    COMPETENCIAFINAL = {empresa.CompetenciaFinal},
                    CALCULADEPRECIACAO = {empresa.CalculoDepreciacao},
                    CALCULADEPRECIACAODOLLAR = {empresa.CalculoDepreciacaoDollar},
                    DEPRECIAMESAQUISICAO = {empresa.DepreciaMesAquisicao},
                    DEPRECIAMESAQUISICAODOLLAR = {empresa.DepreciaMesAquisicaoDollar}
                WHERE EMPRESAID = {empresa.EmpresaId}
                """, cancellationToken);

        if (alterado == 0)
            return false;        

        if (alterado != 1)
            throw new InvalidOperationException($"Atualização afetou {alterado} registros. Era esperado apenas um.");                

        //Log para registros na base 
        //insert

        await transacao.CommitAsync(cancellationToken);
        return true;

    }

    public async Task<bool> DeletarEmpresa(string empresaId, CancellationToken cancellationToken)
    {
        var linhasAfetadas = await _context.Empresas
            .Where(e => e.EmpresaId == empresaId)
            .ExecuteDeleteAsync(cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
        return linhasAfetadas > 0;
    }
}
