using Patrimonio.Data.DTO.CentroCusto;
using Patrimonio.Models;
using Patrimonio.Models.ValueObjects;

namespace Patrimonio.Services;

public interface ICentroCustoService
{
    Task<List<ObtemCentroCustoDTO>> ObterCentroCustoAsync(CentroCustoConsultaDTO dto, CancellationToken cancellationToken);
    Task<bool> ValidarCentroCustoAsync(ValidarCentroCustoDTO dto, CancellationToken cancellationToken);
}
