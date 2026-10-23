using System.ComponentModel.DataAnnotations;

namespace Patrimonio.Data.DTO.CentroCusto;

public class ValidarCentroCustoDTO
{
    [Required]
    public string CentroCustoId { get; set; } = string.Empty;
    [Required]
    public string EmpresaId { get; set; } = string.Empty;
    [Required]
    public string EmpresaFilialId { get; set; } = string.Empty;
}
