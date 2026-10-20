using Microsoft.AspNetCore.Mvc;
using Patrimonio.Data.DTO;
using Patrimonio.Services;

namespace Patrimonio.Controllers;

[ApiController]
[Route("api/[controller]/v1")]
public class EmpresaController : ControllerBase
{
    private readonly ILogger<EmpresaController> _logger;
    private readonly IEmpresaService _empresaService;

    public EmpresaController(ILogger<EmpresaController> logger, IEmpresaService empresaService)
    {
        _logger = logger;
        _empresaService = empresaService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<EmpresaResumoDTO>), StatusCodes.Status200OK)]   
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> ObterEmpresas(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Obtendo empresas...");
        
        var empresas = await _empresaService.ObterEmpresasAsync(cancellationToken);
        
        _logger.LogInformation("Consulta concluída com sucesso.");

        return Ok(empresas);

    }

    [HttpGet("{empresaId}")]
    [ProducesResponseType(typeof(EmpresaDetalheDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<EmpresaDetalheDTO>> ObterEmpresaPorId(
        [FromRoute] string empresaId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando consulta à empresa {empresaId}...", empresaId);

        var empresa = await _empresaService.ObterEmpresaPorIdAsync(empresaId, cancellationToken);

        _logger.LogInformation("Empresa {empresaId} consultada com sucesso.", empresaId);

        return Ok(empresa);
    }
}
