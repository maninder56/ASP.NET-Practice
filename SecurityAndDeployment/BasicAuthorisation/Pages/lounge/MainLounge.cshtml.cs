using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BasicAuthorisation.Pages.lounge;

[Authorize]
public class MainLoungeModel : PageModel
{
    

    public MainLoungeModel()
    {
        
    }


    // Data shared to view 
    public string? UserName { get; private set; }

    public void OnGet()
    {
        UserName = User.Identity?.Name;
    }
}
