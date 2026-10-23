using Patrimonio.Models;
using Patrimonio.Models.ValueObjects;

namespace Patrimonio.Repositories;

public interface ICentroCustoRepository
{
    Task<List<CentroCusto>> ObterCentroCustoAsync(ObterCentroCustoFiltro filtro, CancellationToken cancellationToken);
    Task<bool> ValidarCentroCustoAsync(ValidarCentroCustoFiltro filtro, CancellationToken cancellationToken);
}
