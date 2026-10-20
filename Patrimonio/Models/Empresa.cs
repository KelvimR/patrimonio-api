using Patrimonio.Models.ValueObjects;

namespace Patrimonio.Models;

public class Empresa
{  
    public string EmpresaId { get; private set; }
    public string RazaoSocial { get; private set; }
    public Cgc? CGC { get; private set; }
    public string? Endereco { get; private set; }
    public string? Telefone { get; private set; }
    public string? Ramal { get; private set; }
    public string? FAX { get; private set; }
    public string? Cidade { get; private set; }
    public string? Estado { get; private set; }
    public string? CEP { get; private set; }
    public int? PlacaCorporea { get; private set; }
    public int? PlacaNaoCorporea { get; private set; }
    public string? CalculoDepreciacao { get; private set; }
    public string? CalculoDepreciacaoDollar { get; private set; }
    public string? DepreciaMesAquisicao { get; private set; }
    public string? DepreciaMesAquisicaoDollar { get; private set; }
    public DateTime? CompetenciaInicial { get; private set; }
    public DateTime? CompetenciaFinal { get; private set; }
    public string? TipoCalculo { get; private set; }
    public string? FilialCentralizadora { get; private set; }

    public Empresa(string empresaId, string razaoSocial)
    {
        EmpresaId = ValidaEmpresaId(empresaId);
        RazaoSocial = ValidaRazaoSocial(razaoSocial);
    }
    
    public void AlterarRazaoSocial(string novaRazaoSocial)
    {
        RazaoSocial = ValidaRazaoSocial(novaRazaoSocial);        
    }

    public void DefinirCompetencia(DateTime competenciaInicial, DateTime competenciaFinal)
    {
        if(competenciaFinal < competenciaInicial)
            throw new ArgumentException("A competência final não pode ser anterior à competência inicial.");

        CompetenciaInicial = competenciaInicial;
        CompetenciaFinal = competenciaFinal;
    }

    private static string ValidaEmpresaId(string empresaId)
    {
        if (string.IsNullOrWhiteSpace(empresaId))
            throw new ArgumentException("O identificador da empresa é obrigatório.", nameof(empresaId));

        return empresaId.Trim();
    }

    private static string ValidaRazaoSocial(string razaoSocial)
    {
        if (string.IsNullOrWhiteSpace(razaoSocial))
            throw new ArgumentException("A razão social da empresa é obrigatória.", nameof(razaoSocial));

        return razaoSocial.Trim();
    }
}
