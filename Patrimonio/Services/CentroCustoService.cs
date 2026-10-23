using Patrimonio.Data.DTO.CentroCusto;
using Patrimonio.Models;
using Patrimonio.Models.ValueObjects;
using Patrimonio.Repositories;
using Patrimonio.Repositories.Implementations;
using Patrimonio.Services.Mappings;
using System.ComponentModel.DataAnnotations;

namespace Patrimonio.Services;

public class CentroCustoService : ICentroCustoService
{
    private readonly ICentroCustoRepository _centroCustoRepository;

    public CentroCustoService(ICentroCustoRepository centroCustoRepository)
    {
        _centroCustoRepository = centroCustoRepository;
    }

    public async Task<List<ObtemCentroCustoDTO>> ObterCentroCustoAsync(CentroCustoConsultaDTO dto, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(dto);

        var filtro = new ObterCentroCustoFiltro
        (
            Empresa: dto.EmpresaId,
            EmpresaFilial: dto.EmpresaFilialId,
            Inventario: dto.InventarioId 
        );

        var ccusto = await _centroCustoRepository.ObterCentroCustoAsync(filtro, cancellationToken);
        if (ccusto is null)
            throw new KeyNotFoundException($"Ocorreu um erro ao buscar os centros de custo.");

        return ccusto
            .Select(ccusto => ccusto.ToDTO()).ToList();
    }

    public async Task<bool> ValidarCentroCustoAsync(ValidarCentroCustoDTO dto, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(dto);

        var filtro = new ValidarCentroCustoFiltro
        (
            Empresa: dto.EmpresaId,
            EmpresaFilial: dto.EmpresaFilialId,
            CentroCusto: dto.CentroCustoId
        );

        var validaItem = await _centroCustoRepository.ValidarCentroCustoAsync(filtro, cancellationToken);
        if (!validaItem)
            throw new KeyNotFoundException($"Centro de custo {dto.CentroCustoId} não encontrado no cadastro.");

        return validaItem;
    }
}
