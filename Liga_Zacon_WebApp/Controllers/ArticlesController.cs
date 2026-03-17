using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.Services;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Liga_Zacon_WebApp.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly IArticleService _articleService;

        public ArticlesController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        // GET: Articles
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            return View(await _articleService.GetPagedListAsync(cancellationToken: cancellationToken)); //  Enumerable.Empty<Article>()
        }

        // GET: Articles/Details/5
        public async Task<IActionResult> Details(int? id, CancellationToken cancellationToken)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _articleService.GetByIdAsync(id.Value, cancellationToken);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // GET: Articles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Articles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Text,CreatedAt,IsPublished,Tag")] Article article, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                await _articleService.AddAsync(article, cancellationToken);
                return RedirectToAction(nameof(Index));
            }
            return View(article);
        }

        // GET: Articles/Edit/5
        public async Task<IActionResult> Edit(int? id, CancellationToken cancellationToken)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _articleService.GetByIdAsync(id.Value, cancellationToken);
            if (article == null)
            {
                return NotFound();
            }
            return View(article);
        }

        // POST: Articles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Text,CreatedAt,IsPublished,Tag")] Article article, CancellationToken cancellationToken)
        {
            if (id != article.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var updated = await _articleService.UpdateAsync(article, cancellationToken);
                if (!updated)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(article);
        }

        // GET: Articles/Delete/5
        public async Task<IActionResult> Delete(int? id, CancellationToken cancellationToken)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _articleService.GetByIdAsync(id.Value, cancellationToken);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken)
        {
            var deleted = await _articleService.RemoveAsync(id, cancellationToken);

            if (!deleted)
                throw new Exception();

            return RedirectToAction(nameof(Index));
        }

        // GET: List By Tags
        public async Task<IActionResult> ListByTags(CancellationToken cancellationToken)
        {
            ViewData["Tags"] = await _articleService.GetAllTagsAsync(cancellationToken);
            return View(await _articleService.GetPagedListAsync(cancellationToken: cancellationToken)); //  Enumerable.Empty<Article>()
        }
    }
}
