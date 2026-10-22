using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Patrimonio.Context;
using Patrimonio.Data.DTO;
using Patrimonio.Models;
using Patrimonio.Models.ValueObjects;
using System.Runtime.ConstrainedExecution;
using System.Text.RegularExpressions;

namespace Patrimonio.Repositories.Implementations;

public class EmpresaRepository : IEmpresaRepository
{
    private readonly OracleContext _context;    
    private readonly IAuditoriaRepository _auditoria;
    private readonly string _schema;

    public EmpresaRepository(OracleContext context, IAuditoriaRepository auditoria)
    {
        _context = context;        
        _auditoria = auditoria;
        _schema = _context.ObterSchema();
    }

    public async Task<Empresa> CriarEmpresaAsync(Empresa empresa, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(empresa);
        
        await using var transacao = await _context.Database.BeginTransactionAsync(cancellationToken);

        var incluiEmpresa = await _context.Database.ExecuteSqlInterpolatedAsync($"""
            INSERT INTO {_schema}.EMPRESAS (
                EMPRESAID, RAZAOSOCIAL, ENDERECO, TELEFONE, RAMAL, FAX, CIDADE, ESTADO, CEP, CGC, COMPETENCIAINICIAL, COMPETENCIAFINAL,
                CALCULADEPRECIACAO, CALCULADEPRECIACAODOLLAR, DEPRECIAMESAQUISICAO, DEPRECIAMESAQUISICAODOLLAR, TIPO_CALCULO, FILIAL_CENTRALIZADORA)
            VALUES(
                {empresa.EmpresaId}, {empresa.RazaoSocial}, {empresa.Endereco}, {empresa.Telefone}, {empresa.Ramal}, {empresa.FAX}, {empresa.Cidade},
                {empresa.Estado}, {empresa.CEP}, {empresa.CGC?.Numero}, {empresa.CompetenciaInicial}, {empresa.CompetenciaFinal}, {empresa.CalculoDepreciacao?.Valor},
                {empresa.CalculoDepreciacaoDollar?.Valor}, {empresa.DepreciaMesAquisicao?.Valor}, {empresa.DepreciaMesAquisicaoDollar?.Valor}, {empresa.TipoCalculo}, {empresa.FilialCentralizadora})
            """);

        if (incluiEmpresa != 1)
            throw new InvalidOperationException($"Ocorreu um erro durante a criação da empresa {empresa.EmpresaId}.");

        await _auditoria.RegistrarLogAsync(new Auditoria
        {
            Operacao = $"API Patrimonio - Inclusão da empresa {empresa.EmpresaId}",
            Usuario = "KelvimSantos",
            EmpresaId = empresa.EmpresaId
        },cancellationToken);

        await transacao.CommitAsync(cancellationToken);
        return empresa;
    }

    public Task<List<Empresa>> ObterEmpresasAsync(CancellationToken cancellationToken)
    {
        return _context.Empresas
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Empresa?> ObterEmpresaPorIdAsync(string empresaId, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(empresaId))
            throw new ArgumentException("O identificador da empresa é obrigatório.", nameof(empresaId));

        var empresas = await _context.Empresas
            .FromSqlInterpolated($"""
                SELECT EMPRESAID, RAZAOSOCIAL, CGC, ENDERECO, TELEFONE, RAMAL, FAX, CIDADE, ESTADO, CEP, 
                    PLACACORPOREA, PLACANAOCORPOREA, CALCULADEPRECIACAO, CALCULADEPRECIACAODOLLAR, DEPRECIAMESAQUISICAO, 
                    DEPRECIAMESAQUISICAODOLLAR, COMPETENCIAINICIAL, COMPETENCIAFINAL, TIPO_CALCULO, FILIAL_CENTRALIZADORA
                FROM {_schema}.EMPRESAS
                WHERE EMPRESAID = {empresaId} 
                """)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

        return empresas.FirstOrDefault();
    }

    public async Task<bool> AtualizarEmpresaAsync(Empresa empresa, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(empresa);
        await using var transacao = await _context.Database.BeginTransactionAsync(cancellationToken);        
        
        var alterado = await _context.Database.ExecuteSqlInterpolatedAsync($"""
                UPDATE {_schema}.EMPRESAS SET 
                    RAZAOSOCIAL = {empresa.RazaoSocial},
                    ENDERECO = {empresa.Endereco},
                    TELEFONE = {empresa.Telefone},
                    RAMAL = {empresa.Ramal},
                    FAX = {empresa.FAX},
                    CIDADE = {empresa.Cidade},
                    ESTADO = {empresa.Estado},
                    CEP = {empresa.CEP},
                    CGC = {empresa.CGC?.Numero},
                    FILIAL_CENTRALIZADORA = {empresa.FilialCentralizadora},
                    COMPETENCIAINICIAL = {empresa.CompetenciaInicial},
                    COMPETENCIAFINAL = {empresa.CompetenciaFinal},
                    CALCULADEPRECIACAO = {empresa.CalculoDepreciacao?.Valor},
                    CALCULADEPRECIACAODOLLAR = {empresa.CalculoDepreciacaoDollar?.Valor},
                    DEPRECIAMESAQUISICAO = {empresa.DepreciaMesAquisicao?.Valor},
                    DEPRECIAMESAQUISICAODOLLAR = {empresa.DepreciaMesAquisicaoDollar?.Valor},
                    TIPO_CALCULO = {empresa.TipoCalculo}
                WHERE EMPRESAID = {empresa.EmpresaId}
                """, cancellationToken);

        if (alterado == 0)
            return false;        

        if (alterado != 1)
            throw new InvalidOperationException($"Atualização afetou {alterado} registro(s). Era esperado apenas um.");

        //Log para registros na base 
        await _auditoria.RegistrarLogAsync(new Auditoria
        {
            Operacao = $"API Patrimonio - Atualização da empresa {empresa.EmpresaId}",
            Usuario = "KelvimSantos",
            EmpresaId = empresa.EmpresaId            
        }, cancellationToken);

        await transacao.CommitAsync(cancellationToken);
        return true;

    }

    public async Task<bool> DeletarEmpresaAsync(string empresaId, CancellationToken cancellationToken)
    {

        if (string.IsNullOrWhiteSpace(empresaId))
            throw new ArgumentException("O identificador da empresa é obrigatório.", nameof(empresaId));

        await using var transacao = await _context.Database.BeginTransactionAsync(cancellationToken);

        var linhasAfetadas = await _context.Empresas
            .Where(e => e.EmpresaId == empresaId)
            .ExecuteDeleteAsync(cancellationToken);

        if (linhasAfetadas == 0)
            return false;

        if(linhasAfetadas != 1)
            throw new InvalidOperationException($"Exclusão afetou {linhasAfetadas} registro(s). Era esperado apenas um.");

        await _auditoria.RegistrarLogAsync(new Auditoria
        {
            Operacao = $"API Patrimonio - Exclusão da empresa {empresaId}",
            Usuario = "KelvimSantos",
            EmpresaId = empresaId
        }, cancellationToken);

        await transacao.CommitAsync(cancellationToken);
        return true;
    }
}
