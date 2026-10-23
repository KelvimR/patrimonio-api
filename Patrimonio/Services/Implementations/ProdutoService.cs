using Patrimonio.Data.DTO.Produto;
using Patrimonio.Models;
using Patrimonio.Repositories;
using Patrimonio.Repositories.Implementations;
using Patrimonio.Services.Mappings;

namespace Patrimonio.Services.Implementations;

public class ProdutoService : IProdutoService
{
    private readonly IProdutoRepository _produtoRepository;
    public ProdutoService(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    public async Task<List<ProdutoDTO>> ObterProdutosAsync(CancellationToken cancellationToken)
    {
        var produtos = await _produtoRepository.ObterProdutosAsync(cancellationToken);
        return produtos
            .Select(produto => produto.ToResumoDTO()).ToList();
    }

    public async Task<IReadOnlyList<ProdutoDTO>> ObterProdutosPorDescricaoAsync(string produto, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(produto))
            throw new ArgumentException($"Descrição do produto não informada.", nameof(produto));

        var produtos = await _produtoRepository.ObterProdutosPorDescricaoAsync(produto.Trim(), cancellationToken);

        return produtos.Select(p => p.ToDTO()).ToList();
                   
    }

    public async Task<ProdutoDTO?> ObterProdutosPorIdAsync(int produtoId, CancellationToken cancellationToken)
    {
        var produto = await _produtoRepository.ObterProdutosPorIdAsync(produtoId, cancellationToken);
        if (produto is null)
            throw new KeyNotFoundException($"Produto {produtoId} não encontrado.");

        return produto.ToResumoDTO();
    }
}
