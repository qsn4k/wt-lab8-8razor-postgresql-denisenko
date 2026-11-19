using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using wt_lab8_8razor_postgresql_denisenko.Data;
using wt_lab8_8razor_postgresql_denisenko.Models;
using Microsoft.EntityFrameworkCore;

namespace wt_lab8_8razor_postgresql_denisenko.Pages.Players;

public class EditModel(AppDbContext context) : PageModel
{
    [BindProperty]
    public Player Player { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var existing = await context.Players.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (existing is null)
            return NotFound();

        Player = existing;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        if (id != Player.Id)
            return BadRequest();

        if (!ModelState.IsValid)
            return Page();

        var existing = await context.Players.FindAsync(id);
        if (existing is null)
            return NotFound();

        existing.Nickname = Player.Nickname;
        existing.Game = Player.Game;
        existing.TeamName = Player.TeamName;
        existing.Rating = Player.Rating;
        existing.Wins = Player.Wins;
        existing.Losses = Player.Losses;
        existing.Description = Player.Description;

        await context.SaveChangesAsync();
        TempData["SuccessMessage"] = $"Данные игрока «{existing.Nickname}» обновлены.";
        return RedirectToPage("./Details", new { id = existing.Id });
    }
}
