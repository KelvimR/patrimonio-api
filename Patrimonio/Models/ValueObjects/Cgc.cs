using System.Text.RegularExpressions;

namespace Patrimonio.Models.ValueObjects;

public sealed record Cgc
{
    public string Numero { get; }
    public Cgc(string numero)
    {
        if(string.IsNullOrEmpty(numero))
            throw new ArgumentException("O CNPJ é obrigatório.", nameof(numero));

        var somenteDigitos = Regex.Replace(numero, @"[^\d]", "");
        if(somenteDigitos.Length != 14)
            throw new ArgumentException("O CNPJ deve conter 14 dígitos.", nameof(numero));
        
        Numero = numero;
    }
  
    public override string ToString() => Numero;
}
