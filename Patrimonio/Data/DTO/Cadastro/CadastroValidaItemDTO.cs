using System.ComponentModel.DataAnnotations;

namespace Patrimonio.Data.DTO.Cadastro;

public class CadastroValidaItemDTO
{
    [Required]
    public string Empresa { get; set; } = string.Empty;    
    [Required]
    public long NumeroPatrimonial { get; set; }
}
