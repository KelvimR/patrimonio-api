using Microsoft.AspNetCore.Mvc;
using Patrimonio.Data.DTO.Empresa;
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
        var empresas = await _empresaService.ObterEmpresasAsync(cancellationToken);
        return Ok(empresas);

    }

    [HttpGet("{empresaId}")]
    [ProducesResponseType(typeof(EmpresaDetalheDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<EmpresaDetalheDTO>> ObterEmpresaPorId(
        [FromRoute] string empresaId, CancellationToken cancellationToken)
    {

        var empresa = await _empresaService.ObterEmpresaPorIdAsync(empresaId, cancellationToken);
        return Ok(empresa);

    }

    [HttpPost]
    [ProducesResponseType(typeof(EmpresaDetalheDTO), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CriarEmpresa(
        [FromBody] EmpresaInsereDTO dto, CancellationToken cancellation)
    {
        var empresa = await _empresaService.CriarEmpresaAsync(dto, cancellation);
        return CreatedAtAction(
            nameof(ObterEmpresaPorId),
            new { empresaId = empresa.Empresa}, empresa);        
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AtualizarEmpresa(
        [FromBody] EmpresaUpdateDTO empresa, CancellationToken cancellationToken)
    {
        await _empresaService.AtualizarEmpresaAsync(empresa, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{empresaId}")]
    [ProducesResponseType(typeof(EmpresaDetalheDTO), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<EmpresaDetalheDTO>> DeletarEmpresa(
        [FromRoute] string empresaId, CancellationToken cancellationToken)
    {     
        await _empresaService.DeletarEmpresa(empresaId, cancellationToken);                
        return NoContent();
    }
}
