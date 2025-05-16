using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingList.Models;
using ShoppingList.Services;
using System.ComponentModel.DataAnnotations;

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

    public bool ItemFailedToDelete { get; private set; }


    // Data from view
    [BindProperty]
    [FromForm]
    public InputModel? Input {  get; set; }

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

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            ItemFailedToDelete = true;
            return Page(); 
        }

        if (Input is InputModel inputModel)
        {
            bool deleted = service.DeleteItemByID(inputModel.ID);

            if (deleted)
            {
                return RedirectToPage("Home"); 
            }
        }

        ItemFailedToDelete = true; 
        return Page();
    }

    public class InputModel
    {
        [Required]
        public required int ID { get; set; }
    }
}
