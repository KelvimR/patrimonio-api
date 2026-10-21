using Patrimonio.Models.ValueObjects;

namespace Patrimonio.Models;

public class Empresa
{  
    public string EmpresaId { get;  set; }
    public string RazaoSocial { get;  set; }
    public Cgc? CGC { get;  set; }
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
    public DateTime? CompetenciaInicial { get;  set; }
    public DateTime? CompetenciaFinal { get;  set; }
    public string? TipoCalculo { get;  set; }
    public string? FilialCentralizadora { get;  set; }

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
        DateTime hoje = DateTime.Today;

        if (competenciaInicial == default)
            competenciaInicial = new DateTime(hoje.Year, hoje.Month, 1);

        if (competenciaFinal == default)
            competenciaFinal = new DateTime(competenciaInicial.Year, competenciaInicial.Month, 1).AddMonths(1).AddDays(-1);

        if(competenciaFinal < competenciaInicial)
            throw new ArgumentException("A competência final não pode ser anterior à competência inicial.", nameof(competenciaFinal));

        CompetenciaInicial = competenciaInicial;
        CompetenciaFinal = competenciaFinal;
    }    

     static string ValidaEmpresaId(string empresaId)
    {
        if (string.IsNullOrWhiteSpace(empresaId))
            throw new ArgumentException("O identificador da empresa é obrigatório.", nameof(empresaId));

        return empresaId.Trim();
    }

     static string ValidaRazaoSocial(string razaoSocial)
    {
        if (string.IsNullOrWhiteSpace(razaoSocial))
            throw new ArgumentException("A razão social da empresa é obrigatória.", nameof(razaoSocial));

        return razaoSocial.Trim();
    }
}
