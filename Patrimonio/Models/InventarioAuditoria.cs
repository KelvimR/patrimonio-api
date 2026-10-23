namespace Patrimonio.Models;

public class InventarioAuditoria
{
    public int InventarioId { get; set; }
    public string EmpresaId { get; set; } = string.Empty;
    public string EmpresaFilialId { get; set; } = string.Empty;
    public string CentroCustoId { get; set; } = string.Empty;
    public long NumeroPatrimonial { get; set; }
    public int TipoIncorporacaoId { get; set; }
    public int Incorporacao { get; set; }
    public DateTime DataAquisicao { get; set; }
    public string DescricaoResumida { get; set; } = string.Empty;
    public string ContaId { get; set; } = string.Empty;
    public double ValorAtualizado { get; set; }
    public double DepreciacaoAcumulada { get; set; }
    public double ValorResidual { get; set; }
    public int ProdutoId { get; set; }
    public string DescricaoProduto { get; set; } = string.Empty;
    public string Achoubem { get; set; } = string.Empty;
    public string NovaDescricao { get; set; } = string.Empty;
    public string NovoCentroCusto { get; set; } = string.Empty;
    public long NovaPlaqueta { get; set; } 
    public string Validacao { get; set; } = string.Empty;
    public string UsuarioDefinicao { get; set; } = string.Empty;
    public DateTime DataDefinicao { get; set; }   
    public string Recomendacoes { get; set; } = string.Empty;
    public string Observacao { get; set; } = string.Empty;
    public string Situacao { get; set; } = string.Empty;
    public string FotoPlaqueta { get; set; } = string.Empty;
    public string FotoEquipamento { get; set; } = string.Empty;
}
