using Microsoft.AspNetCore.Mvc;
using Patrimonio.Data.DTO.Cadastro;
using Patrimonio.Data.DTO.Empresa;
using Patrimonio.Services;

namespace Patrimonio.Controllers;

[ApiController]
[Route("api/[controller]/v1")]
public class CadastroController : Controller
{
    private readonly ICadastroService _cadastroService;
    public CadastroController(ICadastroService cadastroService)
    {
        _cadastroService = cadastroService;
    }

    [HttpGet("ObterItemNoCadastro")]
    [ProducesResponseType(typeof(CadastroConsultaDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> ObterItemNoCadastro([FromQuery] CadastroConsultaDTO dto, CancellationToken cancellationToken)
    {
        var item = await _cadastroService.BuscaItemNoCadastroAsync(dto, cancellationToken);
        return Ok(item);
    }

    [HttpGet("ValidarItemNoCadastro")]
    [ProducesResponseType(typeof(CadastroValidaItemDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> ValidarItemNoCadastro([FromQuery] CadastroValidaItemDTO dto, CancellationToken cancellationToken)
    {
        var item = await _cadastroService.ValidaPlaquetaNoCadastroAsync(dto, cancellationToken);
        return Ok(item);
    }
}
