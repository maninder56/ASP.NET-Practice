using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ToDoApp.Pages
{
    public class MissingPageModel : PageModel
    {
        public string? errorCode { get; private set; }

        public IActionResult OnGet(string? error)
        {
            errorCode = error; 
            return Page();
        }
    }
}
