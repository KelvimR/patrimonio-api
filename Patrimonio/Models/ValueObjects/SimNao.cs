namespace Patrimonio.Models.ValueObjects;

public sealed record SimNao
{
    public string Valor { get; }

    private SimNao(string valor)
    {
        Valor = valor;
    }

    public static SimNao Criar(string valor)
    {
        if (string.IsNullOrWhiteSpace(valor))
            throw new ArgumentException("O valor deve ser S ou N.");

        var normalizado = valor.Trim().ToUpperInvariant();
        if (normalizado is not ("S" or "N"))
            throw new ArgumentException("O valor deve ser S ou N.");

        return new SimNao(normalizado);
    }

    public bool EhSim => Valor == "S";
    public bool EhNao => Valor == "N";
}
