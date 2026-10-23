using Patrimonio.Data.DTO.Empresa;
using Patrimonio.Models;
using Patrimonio.Models.ValueObjects;
using System.Security.Cryptography.X509Certificates;

namespace Patrimonio.Services.Mappings;

public static class EmpresaMapper
{
    public static EmpresaResumoDTO ToResumoDTO(this Empresa empresa)
    {

        ArgumentNullException.ThrowIfNull(empresa);

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
            CalculoDepreciacao = empresa.CalculoDepreciacao?.Valor,
            CalculoDepreciacaoDollar = empresa.CalculoDepreciacaoDollar?.Valor,
            DepreciaMesAquisicao = empresa.DepreciaMesAquisicao?.Valor,
            DepreciaMesAquisicaoDollar = empresa.DepreciaMesAquisicaoDollar?.Valor,
            PlacaCorporea = empresa.PlacaCorporea,
            PlacaNaoCorporea = empresa.PlacaNaoCorporea,
            
            CompetenciaInicial = empresa.CompetenciaInicial.HasValue 
                ? DateOnly.FromDateTime(empresa.CompetenciaInicial.Value)  : null,
            
            CompetenciaFinal = empresa.CompetenciaFinal.HasValue 
                ? DateOnly.FromDateTime(empresa.CompetenciaFinal.Value) : null,

            FilialCentralizadora = empresa.FilialCentralizadora,
            TipoCalculo = empresa.TipoCalculo
        };
    }

    public static void AplicarAtualizacao(this EmpresaUpdateDTO dto, Empresa empresa)
    {
        ArgumentNullException.ThrowIfNull(dto);

        if (string.IsNullOrWhiteSpace(dto.RazaoSocial))
            throw new ArgumentException("A razão social é obrigatória.", nameof(dto.RazaoSocial));

        empresa.AlterarRazaoSocial(dto.RazaoSocial);
        empresa.DefinirCgc(dto.CNPJ);
        empresa.DefinirFilialCentralizadora(dto.FilialCentralizadora);
        empresa.DefinirTipoCalculo(dto.TipoCalculo);
        empresa.AtualizarContato(dto.Endereco, dto.Cidade, dto.Estado, dto.CEP, dto.Telefone, dto.Ramal, dto.FAX);

        empresa.ConfigurarDepreciacao(
            ConverterSimNao(dto.CalculoDepreciacao),
            ConverterSimNao(dto.CalculoDepreciacaoDollar),
            ConverterSimNao(dto.DepreciaMesAquisicao),
            ConverterSimNao(dto.DepreciaMesAquisicaoDollar)        
        );                 

        if(dto.CompetenciaInicial.HasValue && dto.CompetenciaFinal.HasValue)
            empresa.DefinirCompetencia(
                dto.CompetenciaInicial.Value.ToDateTime(TimeOnly.MinValue),
                dto.CompetenciaFinal.Value.ToDateTime(TimeOnly.MinValue)
            );        
    }    

    public static Empresa ToEntity(this EmpresaInsereDTO dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        if (string.IsNullOrWhiteSpace(dto.EmpresaId))
            throw new ArgumentException("A empresa é obrigatória.", nameof(dto.EmpresaId));

        if (string.IsNullOrWhiteSpace(dto.RazaoSocial))
            throw new ArgumentException("A razão social é obrigatória.", nameof(dto.RazaoSocial));

        var empresa = new Empresa(dto.EmpresaId, dto.RazaoSocial);

        empresa.DefinirCgc(dto.CGC);

        empresa.AtualizarContato(
            dto.Endereco, dto.Cidade, dto.Estado, dto.CEP, dto.Telefone, dto.Ramal, dto.FAX);

        empresa.DefinirFilialCentralizadora(dto.FilialCentralizadora);
        empresa.DefinirTipoCalculo(dto.TipoCalculo);

        empresa.ConfigurarDepreciacao(
            ConverterSimNao(dto.CalculoDepreciacao),
            ConverterSimNao(dto.CalculoDepreciacaoDollar),
            ConverterSimNao(dto.DepreciaMesAquisicao),
            ConverterSimNao(dto.DepreciaMesAquisicaoDollar)
        );

        var hoje = DateTime.Today;
        var inicio = new DateTime(hoje.Year, hoje.Month, 1);
        var fim = new DateTime(hoje.Year, hoje.Month, DateTime.DaysInMonth(hoje.Year, hoje.Month));

        empresa.DefinirCompetencia(inicio, fim);

        return empresa;
    }

    private static SimNao? ConverterSimNao(string? valor)
    {
        return valor is null
            ? null
            : SimNao.Criar(valor);
    }
}
