using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using wt_lab8_8razor_postgresql_denisenko.Data;
using wt_lab8_8razor_postgresql_denisenko.Models;

namespace wt_lab8_8razor_postgresql_denisenko.Pages.Tournaments;

public class IndexModel(AppDbContext context) : PageModel
{
    public List<Tournament> Tournaments { get; private set; } = new();
    public List<string> Games { get; private set; } = new();
    public List<string> Statuses { get; private set; } = new();
    public string? Search { get; private set; }
    public string? Game { get; private set; }
    public string? Status { get; private set; }

    public async Task OnGetAsync(string? search, string? game, string? status)
    {
        Search = search?.Trim();
        Game = game?.Trim();
        Status = status?.Trim();

        var query = context.Tournaments.AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(Search))
        {
            query = query.Where(t =>
                EF.Functions.ILike(t.Name, $"%{Search}%") ||
                EF.Functions.ILike(t.Game, $"%{Search}%"));
        }

        if (!string.IsNullOrWhiteSpace(Game))
            query = query.Where(t => t.Game == Game);

        if (!string.IsNullOrWhiteSpace(Status))
            query = query.Where(t => t.Status == Status);

        Tournaments = await query
            .OrderBy(t => t.Status == "Идёт" ? 0 : t.Status == "Предстоящий" ? 1 : 2)
            .ThenBy(t => t.StartDate)
            .ThenBy(t => t.Name)
            .ToListAsync();

        Games = await context.Tournaments.AsNoTracking()
            .Select(t => t.Game)
            .Distinct()
            .OrderBy(item => item)
            .ToListAsync();

        Statuses = new List<string> { "Идёт", "Предстоящий", "Завершён" };
    }
}
