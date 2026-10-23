using Patrimonio.Data.DTO.Cadastro;
using Patrimonio.Models;
using Patrimonio.Models.ValueObjects;

namespace Patrimonio.Services;

public interface ICadastroService
{
    Task<bool> ValidaPlaquetaNoCadastroAsync(CadastroValidaItemDTO dto, CancellationToken cancellationToken);
    Task<CadastroDTO?> BuscaItemNoCadastroAsync(CadastroConsultaDTO dto, CancellationToken cancellationToken);
}
