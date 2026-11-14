using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using wt_lab8_8razor_postgresql_denisenko.Data;
using wt_lab8_8razor_postgresql_denisenko.Models;

namespace wt_lab8_8razor_postgresql_denisenko.Pages.Players;

public class DetailsModel(AppDbContext context) : PageModel
{
    public Player? Player { get; private set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Player = await context.Players.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        return Player is null ? NotFound() : Page();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var player = await context.Players.FindAsync(id);
        if (player is null)
            return NotFound();

        context.Players.Remove(player);
        await context.SaveChangesAsync();
        TempData["SuccessMessage"] = $"Игрок «{player.Nickname}» удалён.";
        return RedirectToPage("./Index");
    }
}
