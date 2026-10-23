using Patrimonio.Models;

namespace Patrimonio.Repositories;

public interface IProdutoRepository
{    
    Task<List<Produto>> ObterProdutosAsync(CancellationToken cancellationToken);
    Task<Produto?> ObterProdutosPorIdAsync(int produtoId, CancellationToken cancellationToken);
    Task<List<Produto>> ObterProdutosPorDescricaoAsync(string produto, CancellationToken cancellationToken);
}
