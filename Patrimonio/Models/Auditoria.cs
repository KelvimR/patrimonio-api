using Microsoft.AspNetCore.Identity;

namespace Patrimonio.Models;

public class Auditoria
{
    public string? Operacao { get; set; } 
    public string? Usuario { get; set; }
    public string? EmpresaId { get; set; }
    public int NumeroPatrimonial { get; set; } = 0;
    public int TipoIncorporacaoId { get; set; } = 0;
    public int Incorporacao { get; set; } = 0;
    public decimal ValorAtualizado { get; set; } = 0;
    public decimal DepreciacaoAcumulada { get; set; } = 0;
}
