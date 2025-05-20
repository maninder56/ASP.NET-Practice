using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NotesWebApp.Pages;

public class MissingPageModel : PageModel
{
    ILogger<MissingPageModel> logger;

    public MissingPageModel(ILogger<MissingPageModel> logger)
    {
        this.logger = logger;
    }

    // Data shared with view
    public string? Error { get; private set; }

    public void OnGet(string? error)
    {
        logger.LogWarning("{Error} Error, Invalid URL may have requested", error); 
        Error = error; 
    }
}
