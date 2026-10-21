using Patrimonio.Models.ValueObjects;

namespace Patrimonio.Data.DTO.Empresa;

public class EmpresaDetalheDTO
{
    public string? Empresa { get; set; }
    public string? RazaoSocial { get; set; }
    public Cgc? CNPJ { get; set; }
    public string? Endereco { get;  set; }
    public string? Telefone { get;  set; }
    public string? Ramal { get;  set; }
    public string? FAX { get;  set; }
    public string? Cidade { get;  set; }
    public string? Estado { get;  set; }
    public string? CEP { get;  set; }
    public int? PlacaCorporea { get;  set; }
    public int? PlacaNaoCorporea { get;  set; }
    public string? CalculoDepreciacao { get;  set; }
    public string? CalculoDepreciacaoDollar { get;  set; }
    public string? DepreciaMesAquisicao { get;  set; }
    public string? DepreciaMesAquisicaoDollar { get;  set; }
    public DateOnly? CompetenciaInicial { get;  set; }
    public DateOnly? CompetenciaFinal { get;  set; }
    public string? TipoCalculo { get;  set; }
    public string? FilialCentralizadora { get;  set; }
}
