using Application.Abstractions.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Liga_Zacon_WebApp.ApiControllers;

[ApiController]
[Route("api/articles")]
public class ArticleApiController : Controller
{
    private readonly IArticleService _articleService;

    public ArticleApiController(IArticleService articleService)
    {
        _articleService = articleService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var article = await _articleService.GetByIdAsync(id, cancellationToken);
        if (article == null)
        {
            return NotFound();
        }

        return Ok(article);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var articles = await _articleService.ListAsync(cancellationToken);
        return Ok(articles);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Article article, CancellationToken cancellationToken)
    {
        await _articleService.AddAsync(article, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = article.Id }, article);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Article article, CancellationToken cancellationToken)
    {
        if (id != article.Id)
        {
            return BadRequest();
        }

        var result = await _articleService.UpdateAsync(article, cancellationToken);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await _articleService.RemoveAsync(id, cancellationToken);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpGet("search-by-tag")]
    public async Task<IActionResult> SearchByTag(string tag, CancellationToken cancellationToken)
    {
        var articles = await _articleService.SearchByTagAsync(tag, cancellationToken);
        return Ok(articles);
    }

    [HttpGet("search-by-title")]
    public async Task<IActionResult> SearchByTitle(string title, CancellationToken cancellationToken)
    {
        var articles = await _articleService.SearchByTitleAsync(title, cancellationToken);
        return Ok(articles);
    }

    [HttpPost("paged")]
    public async Task<IActionResult> GetPagedList(int page = 0, int size = 10, string searchTerm = "",
        CancellationToken cancellationToken = default)
    {
        var articles = await _articleService.GetPagedListAsync(page, size, searchTerm, cancellationToken);
        return PartialView("Partial/Table", articles);
    }

    [HttpPost("paged-by-tags")]
    public async Task<IActionResult> GetPagedByTagsList(int page = 0, int size = 10, string tag = "",
        CancellationToken cancellationToken = default)
    {
        var articles = await _articleService.GetPagedByTagsListAsync(page, size, tag, cancellationToken);
        return PartialView("Partial/Table", articles);
    }
}
