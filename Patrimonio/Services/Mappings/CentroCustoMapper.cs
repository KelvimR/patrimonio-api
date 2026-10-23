using Patrimonio.Data.DTO.Cadastro;
using Patrimonio.Data.DTO.CentroCusto;
using Patrimonio.Models;

namespace Patrimonio.Services.Mappings;

public static class CentroCustoMapper
{
    public static ObtemCentroCustoDTO ToDTO(this CentroCusto centroCusto)
    {

        ArgumentNullException.ThrowIfNull(centroCusto);

        return new ObtemCentroCustoDTO
        {            
            CentroCustoId = centroCusto.CentroCustoId,
            Descricao = centroCusto.Descricao
        };
    }
}
