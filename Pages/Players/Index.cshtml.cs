using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using wt_lab8_8razor_postgresql_denisenko.Data;
using wt_lab8_8razor_postgresql_denisenko.Models;

namespace wt_lab8_8razor_postgresql_denisenko.Pages.Players;

public class IndexModel(AppDbContext context) : PageModel
{
    public List<Player> Players { get; private set; } = new();
    public List<string> Games { get; private set; } = new();
    public string? Search { get; private set; }
    public string? Game { get; private set; }

    public async Task OnGetAsync(string? search, string? game)
    {
        Search = search?.Trim();
        Game = game?.Trim();

        var query = context.Players.AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(Search))
            query = query.Where(player => EF.Functions.ILike(player.Nickname, $"%{Search}%"));

        if (!string.IsNullOrWhiteSpace(Game))
            query = query.Where(player => player.Game == Game);

        Players = await query.OrderByDescending(player => player.Rating)
            .ThenBy(player => player.Nickname)
            .ToListAsync();

        Games = await context.Players.AsNoTracking()
            .Select(player => player.Game)
            .Distinct()
            .OrderBy(item => item)
            .ToListAsync();
    }
}
