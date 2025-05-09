using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ShoppingList.Pages
{
    public class MissingPageModel : PageModel
    {

        // Data to share with view 
        public int? ErrorCode { get; private set; }

        public IActionResult OnGet([FromRoute] int? errorCode)
        {
            ErrorCode = errorCode;
            return Page();
        }
    }
}
