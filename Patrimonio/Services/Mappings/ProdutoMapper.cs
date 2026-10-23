using Patrimonio.Data.DTO.Empresa;
using Patrimonio.Data.DTO.Produto;
using Patrimonio.Models;

namespace Patrimonio.Services.Mappings;

public static class ProdutoMapper
{
    public static ProdutoDTO ToResumoDTO(this Produto produto)
    {

        ArgumentNullException.ThrowIfNull(produto);

        return new ProdutoDTO
        {
            ProdutoId = produto.ProdutoId,
            Descricao = produto.Descricao
        };
    }

    public static ProdutoDTO ToDTO(this Produto entity)
    {
        return new ProdutoDTO
        {
            ProdutoId = entity.ProdutoId,
            Descricao = entity.Descricao ?? string.Empty
        };
    }
}
