using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingList.Models;
using ShoppingList.Services;

namespace ShoppingList.Pages; 

public class DeleteItemModel : PageModel
{
    private IShoppingListService service; 

    public DeleteItemModel(IShoppingListService shoppingListService)
    {
        this.service = shoppingListService;
    }


    // Data shared to view 
    public ItemModel? ItemToDelete { get; private set; }
    public int ItemIDFromRoute { get; private set; }
    public bool NoIDProvided { get; private set; }

    public IActionResult OnGet([FromRoute] int? id)
    {
        if (id is int itemID)
        {
            ItemToDelete = service.GetItemByID(itemID);
            ItemIDFromRoute = itemID;
            return Page();
        }

        NoIDProvided = true; 
        return Page();
    }
}
