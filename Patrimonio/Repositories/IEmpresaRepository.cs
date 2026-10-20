using Patrimonio.Data.DTO;
using Patrimonio.Models;

namespace Patrimonio.Repositories;

public interface IEmpresaRepository
{
    Task<Empresa> CriarEmpresaAsync(Empresa empresa, CancellationToken cancellationToken);
    Task<Empresa> AtualizarEmpresaAsync(Empresa empresa, CancellationToken cancellationToken);
    Task<List<Empresa>> ObterEmpresasAsync(CancellationToken cancellationToken);
    Task<Empresa?> ObterEmpresaPorIdAsync(string empresaId, CancellationToken cancellationToken);
    Task DeletarEmpresa(string empresaId, CancellationToken cancellationToken);
}
