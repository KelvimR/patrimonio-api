using System.ComponentModel.DataAnnotations;

namespace Patrimonio.Data.DTO.CentroCusto;

public class CentroCustoConsultaDTO
{
    public int InventarioId { get; set; }
    public string EmpresaId { get; set; } = string.Empty;
    public string EmpresaFilialId { get; set; } = string.Empty;
}
