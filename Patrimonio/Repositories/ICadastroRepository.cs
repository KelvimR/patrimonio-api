using Patrimonio.Models;
using Patrimonio.Models.ValueObjects;

namespace Patrimonio.Repositories;

public interface ICadastroRepository
{
    Task<bool> ValidaPlaquetaNoCadastroAsync(ValidaCadastroFiltro filtro, CancellationToken cancellationToken);
    Task<Cadastro?> BuscaItemNoCadastroAsync(CadastroFiltro filtro, CancellationToken cancellationToken);
}
