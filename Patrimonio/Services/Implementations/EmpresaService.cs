using Patrimonio.Data.DTO.Empresa;
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

    public Task<EmpresaDetalheDTO> CriarEmpresaAsync(EmpresaDetalheDTO empresa, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task AtualizarEmpresaAsync(EmpresaUpdateDTO empresa, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(empresa);
        if (string.IsNullOrWhiteSpace(empresa.Empresa))
            throw new ArgumentException("A empresa é obrigatória.", nameof(empresa.Empresa));

        await ObterEmpresaPorIdAsync(empresa.Empresa, cancellationToken);        
        var entidade = EmpresaMapper.ToUpdateEmpresa(empresa);
        var atualizada = await _empresaRepository.AtualizarEmpresaAsync(entidade, cancellationToken);
        if (!atualizada)
            throw new KeyNotFoundException($"Empresa {empresa.Empresa} não encontrada para atualização.");

        _logger.LogInformation("Empresa {empresa.Empresa} atualizada com sucesso.", empresa.Empresa);
        
    }
        
    public async Task DeletarEmpresa(string empresaId, CancellationToken cancellationToken)
    {
        await ObterEmpresaPorIdAsync(empresaId, cancellationToken);
        var excluida = await _empresaRepository.DeletarEmpresa(empresaId, cancellationToken);

        if (!excluida)
            throw new KeyNotFoundException("Empresa {empresaId} não encontrada ou não foi excluída.");

        if (excluida)
            _logger.LogInformation("Empresa {empresaId} excluida com sucesso.", empresaId);            
    }
}
