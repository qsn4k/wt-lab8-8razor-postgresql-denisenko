using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using wt_lab8_8razor_postgresql_denisenko.Data;
using wt_lab8_8razor_postgresql_denisenko.Models;

namespace wt_lab8_8razor_postgresql_denisenko.Pages;

public class IndexModel(AppDbContext context) : PageModel
{
    public int PlayerCount { get; private set; }
    public int GameCount { get; private set; }
    public int TotalWins { get; private set; }
    public List<Player> TopPlayers { get; private set; } = new();

    public async Task OnGetAsync()
    {
        PlayerCount = await context.Players.CountAsync();
        GameCount = await context.Players.Select(x => x.Game).Distinct().CountAsync();
        TotalWins = await context.Players.SumAsync(x => (int?)x.Wins) ?? 0;
        TopPlayers = await context.Players.AsNoTracking()
            .OrderByDescending(x => x.Rating)
            .ThenBy(x => x.Nickname)
            .Take(3)
            .ToListAsync();
    }
}
