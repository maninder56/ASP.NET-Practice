using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NotesWebApp.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public IActionResult OnGet()
    {
        _logger.LogInformation("Redirect user to Notes page");
        return RedirectToPage("/Notes/AllNotes"); 
    }
}
