using Patrimonio.Models.ValueObjects;
using System.Runtime.CompilerServices;

namespace Patrimonio.Models;

public class Empresa
{
    public string EmpresaId { get; private set; } = null!;
    public string RazaoSocial { get; private set; } = null!;

    // Identificação fiscal
    public Cgc? CGC { get; private set; }
   
    //Contato
    public string? Endereco { get; private  set; }
    public string? Telefone { get; private  set; }
    public string? Ramal { get; private set; }
    public string? FAX { get; private set; }
    public string? Cidade { get; private set; }
    public string? Estado { get; private set; }
    public string? CEP { get; private set; }
    
    
    public int? PlacaCorporea { get;  set; }
    public int? PlacaNaoCorporea { get;  set; }
    public SimNao? CalculoDepreciacao { get;  set; }
    public SimNao? CalculoDepreciacaoDollar { get;  set; }
    public SimNao? DepreciaMesAquisicao { get;  set; }
    public SimNao? DepreciaMesAquisicaoDollar { get;  set; }
    public DateTime? CompetenciaInicial { get; private set; }
    public DateTime? CompetenciaFinal { get; private set; }
    public string? TipoCalculo { get;  set; }
    public string? FilialCentralizadora { get;  set; }

    // Construtor para materialização pelo EF Core
    public Empresa() { }

    public Empresa(string empresaId, string razaoSocial)
    {
        EmpresaId = ValidaEmpresaId(empresaId);
        RazaoSocial = ValidaRazaoSocial(razaoSocial);
    }
    
    public void AlterarRazaoSocial(string novaRazaoSocial)
    {
        RazaoSocial = ValidaRazaoSocial(novaRazaoSocial);        
    }

    public void DefinirCgc(Cgc? cgc)
    {
        CGC = cgc;
    }

    public void DefinirFilialCentralizadora(string? filial)
    {
        FilialCentralizadora = filial;
    }
    
    public void DefinirTipoCalculo(string? tipoCalculo)
    {
        TipoCalculo = tipoCalculo;
    }

    public void DefinirCompetencia(DateTime inicio, DateTime fim)
    {
        if (inicio == default)
            throw new ArgumentException("Competência inicial inválida.",nameof(inicio));

        if(fim == default)
            throw new ArgumentException("Competência final inválida.", nameof(fim));

        if (fim < inicio)
            throw new ArgumentException("Competência final não pode ser anterior à inicial.", nameof(fim));

        CompetenciaInicial = inicio;
        CompetenciaFinal = fim;
    }    

     static string ValidaEmpresaId(string empresaId)
    {
        if (string.IsNullOrWhiteSpace(empresaId))
            throw new ArgumentException("Identificador obrigatório.", nameof(empresaId));

        return empresaId.Trim();
    }

     static string ValidaRazaoSocial(string razaoSocial)
    {
        if (string.IsNullOrWhiteSpace(razaoSocial))
            throw new ArgumentException("Razão social obrigatória.", nameof(razaoSocial));

        return razaoSocial.Trim();
    }

    public void AtualizarContato(
        string? endereco,
        string? cidade,
        string? estado,
        string? cep,
        string? telefone,
        string? ramal,
        string? fax
        )
    {
        Endereco = endereco;
        Cidade = cidade;
        Estado = estado;
        CEP = cep;
        Telefone = telefone;
        Ramal = ramal;
        FAX = fax;
    }

    public void ConfigurarDepreciacao(
        SimNao? calculoDepreciacao,
        SimNao? calculoDepreciacaoDollar,
        SimNao? depreciacaoMesAquisicao,
        SimNao? depreciacaoMesAquisicaoDollar)
    {
        CalculoDepreciacao = calculoDepreciacao;
        CalculoDepreciacaoDollar = calculoDepreciacaoDollar;
        DepreciaMesAquisicao = depreciacaoMesAquisicao;
        DepreciaMesAquisicaoDollar = depreciacaoMesAquisicaoDollar;
    }
}
