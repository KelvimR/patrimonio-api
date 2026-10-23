using Microsoft.AspNetCore.Mvc;
using Patrimonio.Data.DTO.Cadastro;
using Patrimonio.Data.DTO.CentroCusto;
using Patrimonio.Services;
using Patrimonio.Services.Implementations;

namespace Patrimonio.Controllers;

[ApiController]
[Route("api/[controller]/v1")]
public class CentroCustoController : Controller
{

    private readonly ICentroCustoService _centroCustoService;

    public CentroCustoController(ICentroCustoService centroCustoService)
    {
        _centroCustoService = centroCustoService;
    }

    [HttpGet("ObterCentroCusto")]
    [ProducesResponseType(typeof(ObtemCentroCustoDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> ObterItemNoCadastro([FromQuery] CentroCustoConsultaDTO dto, CancellationToken cancellationToken)
    {
        var item = await _centroCustoService.ObterCentroCustoAsync(dto, cancellationToken);
        return Ok(item);
    }

    [HttpGet("ValidarCentroCusto")]
    [ProducesResponseType(typeof(ValidarCentroCustoDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> ValidarItemNoCadastro([FromQuery] ValidarCentroCustoDTO dto, CancellationToken cancellationToken)
    {
        var item = await _centroCustoService.ValidarCentroCustoAsync(dto, cancellationToken);
        return Ok(item);
    }
}
