using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using wt_lab8_8razor_postgresql_denisenko.Data;
using wt_lab8_8razor_postgresql_denisenko.Models;

namespace wt_lab8_8razor_postgresql_denisenko.Pages.Players;

public class TopModel(AppDbContext context) : PageModel
{
    public List<Player> Players { get; private set; } = new();

    public async Task OnGetAsync()
    {
        Players = await context.Players.AsNoTracking()
            .OrderByDescending(x => x.Rating)
            .ThenBy(x => x.Nickname)
            .Take(3)
            .ToListAsync();
    }
}
