using Microsoft.AspNetCore.Mvc;
using Patrimonio.Data.DTO.Empresa;
using Patrimonio.Services;

namespace Patrimonio.Controllers;

[ApiController]
[Route("api/[controller]/v1")]
public class EmpresaController : ControllerBase
{
    private readonly IEmpresaService _empresaService;

    public EmpresaController(IEmpresaService empresaService)
    {
        _empresaService = empresaService;
    }

    [HttpGet("ObterEmpresas")]
    [ProducesResponseType(typeof(List<EmpresaResumoDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> ObterEmpresas(CancellationToken cancellationToken)
    {
        var empresas = await _empresaService.ObterEmpresasAsync(cancellationToken);
        return Ok(empresas);

    }

    [HttpGet("ObterEmpresaPorId/{empresaId}")]
    [ProducesResponseType(typeof(EmpresaDetalheDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<EmpresaDetalheDTO>> ObterEmpresaPorId(
        [FromRoute] string empresaId, CancellationToken cancellationToken)
    {

        var empresa = await _empresaService.ObterEmpresaPorIdAsync(empresaId, cancellationToken);
        return Ok(empresa);

    }

    [HttpPost("CriarEmpresa")]
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


    [HttpPut("AtualizarEmpresa")]
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

    [HttpDelete("DeletarEmpresa/{empresaId}")]
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
