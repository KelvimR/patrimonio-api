using Patrimonio.Data.DTO;

namespace Patrimonio.Services;

public interface IEmpresaService
{
    Task<List<EmpresaResumoDTO>> ObterEmpresasAsync(CancellationToken cancellationToken);
    Task<EmpresaDetalheDTO> ObterEmpresaPorIdAsync(string empresaId, CancellationToken cancellationToken);
    Task<EmpresaDetalheDTO> CriarEmpresaAsync(EmpresaDetalheDTO empresa, CancellationToken cancellationToken);
    Task<EmpresaDetalheDTO> AtualizarEmpresaAsync(EmpresaDetalheDTO empresa, CancellationToken cancellationToken);
    Task DeletarEmpresa(string empresaId, CancellationToken cancellationToken);
}
