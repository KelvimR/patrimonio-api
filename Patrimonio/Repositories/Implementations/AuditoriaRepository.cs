using Microsoft.EntityFrameworkCore;
using Patrimonio.Context;
using Patrimonio.Models;

namespace Patrimonio.Repositories.Implementations;

public class AuditoriaRepository : IAuditoriaRepository
{
    private readonly OracleContext _context;

    public AuditoriaRepository(OracleContext context)
    {
        _context = context;
    }

    public async Task RegistrarLogAsync(Auditoria auditoria, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(auditoria);

        var logIncluido = await _context.Database.ExecuteSqlInterpolatedAsync($"""
            INSERT INTO LOG
                (SEQUENCIA, DATA, OPERACAO, USUARIO, EMPRESAID, NUMEROPATRIMONIAL, TIPOINCORPORACAOID, INCORPORACAO, VALORATUALIZADO, DEPRECIACAOACUMULADA)
                    VALUES (SEQ_PATRIMONIO_LOG.NEXTVAL, {DateTime.UtcNow}, {auditoria.Operacao}, {auditoria.Usuario}, {auditoria.EmpresaId},{auditoria.NumeroPatrimonial}, {auditoria.TipoIncorporacaoId}, 
                    {auditoria.Incorporacao}, {auditoria.ValorAtualizado}, {auditoria.DepreciacaoAcumulada})
            """, cancellationToken);

        if (logIncluido != 1)
            throw new InvalidOperationException($"Inclusão do log afetou {logIncluido} registro(s). Era esperado apenas um.");
    }
}
