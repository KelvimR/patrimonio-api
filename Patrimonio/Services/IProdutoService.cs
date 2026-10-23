using Patrimonio.Data.DTO.Produto;
using Patrimonio.Models;

namespace Patrimonio.Services;

public interface IProdutoService
{
    Task<List<ProdutoDTO>> ObterProdutosAsync(CancellationToken cancellationToken);
    Task<ProdutoDTO?> ObterProdutosPorIdAsync(int produtoId, CancellationToken cancellationToken);
    Task<IReadOnlyList<ProdutoDTO>> ObterProdutosPorDescricaoAsync(string produto, CancellationToken cancellationToken);
}
