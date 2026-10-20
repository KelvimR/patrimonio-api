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
    [ProducesResponseType(200, Type = typeof(List<EmpresaResumoDTO>))]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    public async Task<IActionResult> ObterEmpresas(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Obtendo empresas...");
        
        var empresas = await _empresaService.ObterEmpresasAsync(cancellationToken);
        
        _logger.LogInformation("Consulta concluída com sucesso.");

        return Ok(empresas);

    }
}
