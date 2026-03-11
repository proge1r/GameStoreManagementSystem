using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameStore.Web.Data;
using GameStore.Web.Data.Entities;
using GameStore.Web.Models;

namespace GameStore.Web.Controllers;

public class GamesController : Controller
{
    private readonly AppDbContext _context;

    public GamesController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var games = await _context.Games.Include(g => g.Category).ToListAsync();

        var categories = await _context.Categories.ToListAsync();
        ViewBag.Categories = new SelectList(categories, "Id", "Name");

        return View(games);
    }

    [HttpPost]
    public async Task<IActionResult> InlineAdd(GameInlineAddViewModel model)
    {
        if (ModelState.IsValid)
        {
            var game = new Game
            {
                Name = model.Name,
                Price = model.Price,
                CategoryId = model.CategoryId
            };

            _context.Games.Add(game);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> BulkEdit()
    {
        var model = new GameBulkEditViewModel
        {
            Games = await _context.Games.ToListAsync()
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> BulkUpdate(GameBulkEditViewModel model)
    {
        if (model.Games == null || !model.Games.Any())
        {
            return RedirectToAction(nameof(Index));
        }

        foreach (var gameEntry in model.Games)
        {
            var existingGame = await _context.Games.FindAsync(gameEntry.Id);
            if (existingGame != null)
            {
                existingGame.Name = gameEntry.Name;
                existingGame.Price = gameEntry.Price;
            }
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> CreateFirstCategory(string name)
    {
        if (!string.IsNullOrEmpty(name))
        {
            _context.Categories.Add(new Category { Name = name });
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}