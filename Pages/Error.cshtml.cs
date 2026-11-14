using Microsoft.AspNetCore.Mvc.RazorPages;

namespace wt_lab8_8razor_postgresql_denisenko.Pages;

public class ErrorModel : PageModel
{
    public string RequestId { get; private set; } = string.Empty;

    public void OnGet() => RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier;
}
