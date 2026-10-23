using Patrimonio.Data.DTO.Cadastro;
using Patrimonio.Models;

namespace Patrimonio.Services.Mappings;

public static class CadastroMapper
{
    public static CadastroDTO ToDTO(this Cadastro cadastro)
    {

        ArgumentNullException.ThrowIfNull(cadastro);

        return new CadastroDTO
        {
            EmpresaId = cadastro.EmpresaId,
            EmpresaFilialId = cadastro.EmpresaFilialId,
            NumeroPatrimonial = cadastro.NumeroPatrimonial, 
            TipoIncorporacaoId = cadastro.TipoIncorporacaoId,
            Incorporacao = cadastro.Incorporacao,
            Descricao = cadastro.Descricao,
            CentroCustoId = cadastro.CentroCustoId
        };
    }
}
