using Patrimonio.Data.DTO.Empresa;
using Patrimonio.Models;
using System.Security.Cryptography.X509Certificates;

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
        ArgumentNullException.ThrowIfNull(empresa);

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
            
            CompetenciaInicial = empresa.CompetenciaInicial.HasValue 
                ? DateOnly.FromDateTime(empresa.CompetenciaInicial.Value)  : null,
            
            CompetenciaFinal = empresa.CompetenciaInicial.HasValue 
                ? DateOnly.FromDateTime(empresa.CompetenciaInicial.Value) : null,

            FilialCentralizadora = empresa.FilialCentralizadora,
            TipoCalculo = empresa.TipoCalculo
        };
    }

    public static Empresa ToUpdateEmpresa(this EmpresaUpdateDTO dto)
    {
        ArgumentNullException.ThrowIfNull(dto);
        
        if (string.IsNullOrWhiteSpace(dto.RazaoSocial))
            throw new ArgumentException("A razão social é obrigatória.", nameof(dto.RazaoSocial));

        if (string.IsNullOrWhiteSpace(dto.Empresa))
            throw new ArgumentException("A empresa é obrigatória.", nameof(dto.Empresa));

        return new Empresa(dto.Empresa, dto.RazaoSocial)
        {
            EmpresaId = dto.Empresa,
            RazaoSocial = dto.RazaoSocial,
            CGC = dto.CNPJ,
            FAX = dto.FAX,
            Ramal = dto.Ramal,
            Telefone = dto.Telefone,
            CEP = dto.CEP,
            Cidade = dto.Cidade,
            Endereco = dto.Endereco,
            Estado = dto.Estado,
            CalculoDepreciacao = dto.CalculoDepreciacao,
            CalculoDepreciacaoDollar = dto.CalculoDepreciacaoDollar,
            DepreciaMesAquisicao = dto.DepreciaMesAquisicao,
            DepreciaMesAquisicaoDollar = dto.DepreciaMesAquisicaoDollar,
            FilialCentralizadora = dto.FilialCentralizadora,
            CompetenciaInicial = dto.CompetenciaInicial?.ToDateTime(TimeOnly.MinValue),
            CompetenciaFinal = dto.CompetenciaFinal?.ToDateTime(TimeOnly.MinValue),
        };
    }

    public static Empresa ToInsereEmpresa(this EmpresaInsereDTO dto)
    {
        ArgumentNullException.ThrowIfNull(dto);
        
        if (string.IsNullOrWhiteSpace(dto.EmpresaId))
            throw new ArgumentException("A empresa é obrigatória.", nameof(dto.EmpresaId));

        if (string.IsNullOrWhiteSpace(dto.RazaoSocial))
            throw new ArgumentException("A razão social é obrigatória.", nameof(dto.RazaoSocial));
       
        return new Empresa(dto.EmpresaId, dto.RazaoSocial)
        {
            EmpresaId = dto.EmpresaId,
            RazaoSocial = dto.RazaoSocial,
            CGC = dto.CGC,
            FAX = dto.FAX,
            Ramal = dto.Ramal,
            Telefone = dto.Telefone,
            CEP = dto.CEP,
            Cidade = dto.Cidade,
            Endereco = dto.Endereco,
            Estado = dto.Estado,
            CalculoDepreciacao = dto.CalculoDepreciacao,
            CalculoDepreciacaoDollar = dto.CalculoDepreciacaoDollar,
            DepreciaMesAquisicao = dto.DepreciaMesAquisicao,
            DepreciaMesAquisicaoDollar = dto.DepreciaMesAquisicaoDollar,
            FilialCentralizadora = dto.FilialCentralizadora,
            CompetenciaInicial = dto.CompetenciaInicial,
            CompetenciaFinal = dto.CompetenciaFinal         
        };        
    }
}
