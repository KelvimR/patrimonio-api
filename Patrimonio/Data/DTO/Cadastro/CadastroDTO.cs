namespace Patrimonio.Data.DTO.Cadastro;
public class CadastroDTO
{
    public string? EmpresaId { get; set; }
    public string? EmpresaFilialId { get; set; }
    public long NumeroPatrimonial { get; set; }
    public long TipoIncorporacaoId { get; set; }
    public long Incorporacao { get; set; }
    public string? CentroCustoId { get; set; }
    public string? Descricao { get; set; }
}
