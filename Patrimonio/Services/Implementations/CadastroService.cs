using Patrimonio.Data.DTO.Cadastro;
using Patrimonio.Models;
using Patrimonio.Models.ValueObjects;
using Patrimonio.Repositories;
using Patrimonio.Services.Mappings;

namespace Patrimonio.Services.Implementations;

public class CadastroService : ICadastroService
{
    private readonly ICadastroRepository _cadastroRepository;

    public CadastroService(ICadastroRepository cadastroRepository)
    {
        _cadastroRepository = cadastroRepository;
    }

    public async Task<CadastroDTO?> BuscaItemNoCadastroAsync(CadastroConsultaDTO dto, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(dto);

        var filtro = new CadastroFiltro
        (
            Empresa: dto.Empresa,
            Filial: dto.Filial,
            NumeroPatrimonial: dto.NumeroPatrimonial,
            TipoIncorporacao: dto.TipoIncorporacao,
            Incorporacao: dto.Incorporacao
        );        

        var item = await _cadastroRepository.BuscaItemNoCadastroAsync(filtro, cancellationToken);
        if(item is null)
            throw new KeyNotFoundException($"Item {filtro.NumeroPatrimonial} não encontrado.");

        return item.ToDTO();
    }

    public async Task<bool> ValidaPlaquetaNoCadastroAsync(CadastroValidaItemDTO dto, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(dto);

        var filtro = new ValidaCadastroFiltro
        (
            Empresa: dto.Empresa,
            NumeroPatrimonial: dto.NumeroPatrimonial
        );

        var validaItem = await _cadastroRepository.ValidaPlaquetaNoCadastroAsync(filtro, cancellationToken);
        if(!validaItem)
            throw new KeyNotFoundException($"Item {filtro.NumeroPatrimonial} não encontrado no cadastro.");

        return validaItem;
    }
}
