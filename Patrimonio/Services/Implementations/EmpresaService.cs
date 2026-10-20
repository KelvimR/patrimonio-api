using Patrimonio.Data.DTO;
using Patrimonio.Repositories;
using Patrimonio.Services.Mappings;
using System.Runtime.CompilerServices;

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

        return new EmpresaDetalheDTO
        {
            Empresa = empresa.EmpresaId,
            RazaoSocial = empresa.RazaoSocial,
            CNPJ = empresa.CGC
        };
    }

    public Task<EmpresaDetalheDTO> CriarEmpresaAsync(EmpresaDetalheDTO empresa, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<EmpresaDetalheDTO> AtualizarEmpresaAsync(EmpresaDetalheDTO empresa, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
        
    public async Task DeletarEmpresa(string empresaId, CancellationToken cancellationToken)
    {
        var verificaEmpresa = await ObterEmpresaPorIdAsync(empresaId, cancellationToken);
        await _empresaRepository.DeletarEmpresa(empresaId, cancellationToken);        
    }
}
