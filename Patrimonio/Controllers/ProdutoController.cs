using Microsoft.AspNetCore.Mvc;
using Patrimonio.Data.DTO.Empresa;
using Patrimonio.Data.DTO.Produto;
using Patrimonio.Services;
using Patrimonio.Services.Implementations;

namespace Patrimonio.Controllers;

[ApiController]
[Route("api/[controller]/v1")]
public class ProdutoController : Controller
{
    private readonly IProdutoService _produtoService;
    private readonly ILogger<ProdutoController> _logger;

    public ProdutoController(IProdutoService produtoService, ILogger<ProdutoController> logger)
    {
        _produtoService = produtoService;
        _logger = logger;
    }

    [HttpGet("ObterProdutos")]
    [ProducesResponseType(typeof(List<ProdutoDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> ObterProdutos(CancellationToken cancellationToken)
    {
        var produtos = await _produtoService.ObterProdutosAsync(cancellationToken);
        return Ok(produtos);
    }

    [HttpGet("ObterProdutoPorId/{produtoId}")]
    [ProducesResponseType(typeof(ProdutoDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ProdutoDTO>> ObterProdutoPorId(
        [FromRoute] int produtoId, CancellationToken cancellationToken)
    {
        var produto = await _produtoService.ObterProdutosPorIdAsync(produtoId, cancellationToken);
        return Ok(produto);
    }

    [HttpGet("ObterProdutoPorDescricao/{descricao}")]
    [ProducesResponseType(typeof(ProdutoDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ProdutoDTO>> ObterProdutoPorId(
    [FromRoute] string descricao, CancellationToken cancellationToken)
    {
        var produto = await _produtoService.ObterProdutosPorDescricaoAsync(descricao, cancellationToken);
        return Ok(produto);
    }
}
