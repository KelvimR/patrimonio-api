using System.ComponentModel.DataAnnotations;

namespace Patrimonio.Data.DTO.Cadastro;

public class CadastroConsultaDTO
{
    [Required]
    public string Empresa { get; set; } = string.Empty;

    [Required]
    public string Filial { get; set; } = string.Empty;

    [Required]
    public long NumeroPatrimonial { get; set; }

    [Required]
    public long TipoIncorporacao { get; set; }

    [Required]
    public long Incorporacao { get; set; }
}
