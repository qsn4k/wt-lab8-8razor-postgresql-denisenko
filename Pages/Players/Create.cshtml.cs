using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using wt_lab8_8razor_postgresql_denisenko.Data;
using wt_lab8_8razor_postgresql_denisenko.Models;

namespace wt_lab8_8razor_postgresql_denisenko.Pages.Players;

public class CreateModel(AppDbContext context) : PageModel
{
    [BindProperty]
    public Player Player { get; set; } = new();

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        Player.CreatedAt = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
        context.Players.Add(Player);
        await context.SaveChangesAsync();

        TempData["SuccessMessage"] = $"Игрок «{Player.Nickname}» успешно добавлен.";
        return RedirectToPage("./Index");
    }
}
