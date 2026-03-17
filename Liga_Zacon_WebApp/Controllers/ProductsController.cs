using System.Threading;
using Application.Abstractions.Services;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Liga_Zacon_WebApp.Controllers;

public class ProductsController : Controller
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    // GET: Products
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        return View(await _productService.ListAsync(cancellationToken: cancellationToken));
    }

    // GET: Products/Details/5
    public async Task<IActionResult> Details(int? id, CancellationToken cancellationToken)
    {
        if (id == null)
        {
            return NotFound();
        }

        var product = await _productService.GetByIdAsync(id.Value, cancellationToken);
        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    // GET: Products/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Products/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Title,Price,Description,Category,Image,Rate,Count")] Product product, CancellationToken cancellationToken)
    {
        if (ModelState.IsValid)
        {
            await _productService.AddAsync(product, cancellationToken);
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }

    // GET: Products/Edit/5
    public async Task<IActionResult> Edit(int? id, CancellationToken cancellationToken)
    {
        if (id == null)
        {
            return NotFound();
        }

        var product = await _productService.GetByIdAsync(id.Value, cancellationToken);
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }

    // POST: Products/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Price,Description,Category,Image,Rate,Count")] Product product, CancellationToken cancellationToken)
    {
        if (id != product.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            var updated = await _productService.UpdateAsync(product, cancellationToken);
            if (!updated)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }

    // GET: Products/Delete/5
    public async Task<IActionResult> Delete(int? id, CancellationToken cancellationToken)
    {
        if (id == null)
        {
            return NotFound();
        }

        var product = await _productService.GetByIdAsync(id.Value, cancellationToken);
        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    // POST: Products/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken)
    {
        var deleted = await _productService.RemoveAsync(id, cancellationToken);

        if (!deleted)
            throw new Exception();

        return RedirectToAction(nameof(Index));
    }

}
