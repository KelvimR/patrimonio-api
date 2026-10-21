using Patrimonio.Data.DTO.Empresa;

namespace Patrimonio.Services;

public interface IEmpresaService
{
    Task<List<EmpresaResumoDTO>> ObterEmpresasAsync(CancellationToken cancellationToken);
    Task<EmpresaDetalheDTO> ObterEmpresaPorIdAsync(string empresaId, CancellationToken cancellationToken);
    Task<EmpresaDetalheDTO> CriarEmpresaAsync(EmpresaInsereDTO empresa, CancellationToken cancellationToken);
    Task AtualizarEmpresaAsync(EmpresaUpdateDTO empresa, CancellationToken cancellationToken);
    Task DeletarEmpresa(string empresaId, CancellationToken cancellationToken);
}
