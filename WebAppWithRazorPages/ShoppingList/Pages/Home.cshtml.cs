using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingList.Models;
using ShoppingList.Services;

namespace ShoppingList.Pages; 

public class HomeModel : PageModel
{
    private IShoppingListService service;

    public HomeModel(IShoppingListService shoppingListService)
    {
        this.service = shoppingListService;
    }

    // Data Shared with view 
    public List<ItemModel> ShoppingList { get; private set; } = new List<ItemModel>();

    public IActionResult OnGet()
    {
        ShoppingList = service.GetAllItems();
        return Page();
    }
}
