using Patrimonio.Data.DTO;
using Patrimonio.Models;

namespace Patrimonio.Services.Mappings;

public static class EmpresaMapper
{
    public static EmpresaResumoDTO ToResumoDTO(this Empresa empresa)
    {
        return new EmpresaResumoDTO
        {
            Empresa = empresa.EmpresaId,
            RazaoSocial = empresa.RazaoSocial
        };
    }

    public static EmpresaDetalheDTO ToDetalheDTO(this Empresa empresa)
    {
        return new EmpresaDetalheDTO
        {
            Empresa = empresa.EmpresaId,
            RazaoSocial = empresa.RazaoSocial,
            CNPJ = empresa.CGC,
            CEP = empresa.CEP,
            Endereco = empresa.Endereco,
            Cidade = empresa.Cidade,
            Estado = empresa.Estado,
            FAX = empresa.FAX,
            Ramal = empresa.Ramal,
            Telefone = empresa.Telefone,
            CalculoDepreciacao = empresa.CalculoDepreciacao,
            CalculoDepreciacaoDollar = empresa.CalculoDepreciacaoDollar,
            DepreciaMesAquisicao = empresa.DepreciaMesAquisicao,
            DepreciaMesAquisicaoDollar = empresa.DepreciaMesAquisicaoDollar,
            PlacaCorporea = empresa.PlacaCorporea,
            PlacaNaoCorporea = empresa.PlacaNaoCorporea,
            CompetenciaInicial = empresa.CompetenciaInicial,
            CompetenciaFinal = empresa.CompetenciaFinal,
            FilialCentralizadora = empresa.FilialCentralizadora,
            TipoCalculo = empresa.TipoCalculo
        };
    }
}
