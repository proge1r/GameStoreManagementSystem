using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameStore.Web.Data;
using GameStore.Web.Data.Entities;
using GameStore.Web.Models;

namespace GameStore.Web.Controllers;

public class CategoriesController : Controller
{
    private readonly AppDbContext _context;

    public CategoriesController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> BulkEdit()
    {
        var model = new CategoryBulkEditViewModel
        {
            Categories = await _context.Categories.ToListAsync()
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> InlineAdd(string name)
    {
        if (!string.IsNullOrWhiteSpace(name))
        {
            _context.Categories.Add(new Category { Name = name });
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(BulkEdit));
    }

    [HttpPost]
    public async Task<IActionResult> BulkUpdate(CategoryBulkEditViewModel model)
    {
        if (model.Categories == null || !model.Categories.Any())
        {
            return RedirectToAction(nameof(BulkEdit));
        }

        foreach (var category in model.Categories)
        {
            var dbCategory = await _context.Categories.FindAsync(category.Id);
            if (dbCategory != null)
            {
                dbCategory.Name = category.Name;
            }
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(BulkEdit));
    }
}