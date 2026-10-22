using Patrimonio.Data.DTO.Empresa;
using Patrimonio.Exceptions;
using Patrimonio.Repositories;
using Patrimonio.Services.Mappings;

namespace Patrimonio.Services.Implementations;

public class EmpresaService : IEmpresaService
{

    private readonly IEmpresaRepository _empresaRepository;
    private readonly ILogger<EmpresaService> _logger;

    public EmpresaService(IEmpresaRepository empresaRepository, ILogger<EmpresaService> logger)
    {
        _empresaRepository = empresaRepository;
        _logger = logger;
    }

    public async Task<List<EmpresaResumoDTO>> ObterEmpresasAsync(CancellationToken cancellationToken)
    {
        var empresas = await _empresaRepository.ObterEmpresasAsync(cancellationToken);
        return empresas
            .Select(empresas => empresas.ToResumoDTO()).ToList();
    }

    public async Task<EmpresaDetalheDTO> ObterEmpresaPorIdAsync(string empresaId, CancellationToken cancellationToken)
    {
        var empresa = await _empresaRepository.ObterEmpresaPorIdAsync(empresaId, cancellationToken);
        if (empresa is null)
            throw new KeyNotFoundException($"Empresa {empresaId} não encontrada.");

        return empresa.ToDetalheDTO();
    }

    public async Task<EmpresaDetalheDTO> CriarEmpresaAsync(EmpresaInsereDTO dto, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(dto);
        
        if(string.IsNullOrWhiteSpace(dto.EmpresaId))
            throw new ArgumentException("O identificador da empresa é obrigatório.", nameof(dto.EmpresaId));

        var empresaExiste = await _empresaRepository.ObterEmpresaPorIdAsync(dto.EmpresaId, cancellationToken);
        if (empresaExiste is not null)
            throw new ConflictException($"Empresa {dto.EmpresaId} já cadastrada!");

        var empresaCriada = await _empresaRepository.CriarEmpresaAsync(dto.ToEntity(), cancellationToken);

        _logger.LogInformation("Empresa {empresaCriada.EmpresaId} criada com sucesso.", empresaCriada.EmpresaId);
        return empresaCriada.ToDetalheDTO();                
    }

    public async Task AtualizarEmpresaAsync(EmpresaUpdateDTO dto, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(dto);
        if (string.IsNullOrWhiteSpace(dto.EmpresaId))
            throw new ArgumentException("A empresa é obrigatória.", nameof(dto.EmpresaId));

        var empresa = await _empresaRepository.ObterEmpresaPorIdAsync(dto.EmpresaId, cancellationToken);
        if (empresa is null)
            throw new KeyNotFoundException($"Empresa {dto.EmpresaId } não encontrada.");

        dto.AplicarAtualizacao(empresa);
        
        var atualizada = await _empresaRepository.AtualizarEmpresaAsync(empresa, cancellationToken);
        if (!atualizada)
            throw new KeyNotFoundException($"Empresa {dto.EmpresaId} não encontrada para atualização.");

        _logger.LogInformation("Empresa {empresa.Empresa} atualizada com sucesso.", empresa.EmpresaId);
        
    }
        
    public async Task DeletarEmpresa(string empresaId, CancellationToken cancellationToken)
    {
        if(string.IsNullOrWhiteSpace(empresaId))
            throw new ArgumentException("O identificador da empresa é obrigatório.",nameof(empresaId));

        await ObterEmpresaPorIdAsync(empresaId, cancellationToken);
        var excluida = await _empresaRepository.DeletarEmpresaAsync(empresaId, cancellationToken);

        if (!excluida)
            throw new KeyNotFoundException($"Empresa {empresaId} não encontrada ou não foi excluída.");

        if (excluida)
            _logger.LogInformation("Empresa {empresaId} excluida com sucesso.", empresaId);            
    }
}
