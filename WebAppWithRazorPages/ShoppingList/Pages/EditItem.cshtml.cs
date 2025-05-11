using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingList.Models;
using ShoppingList.Services;
using System.ComponentModel.DataAnnotations;

namespace ShoppingList.Pages; 

public class EditItemModel : PageModel
{
    private IShoppingListService service;

    public EditItemModel(IShoppingListService service)
    {
        this.service = service;
    }


    // Data Shared with view
    public int ItemIDFromRoute { get; private set; }
    public bool NoIDProvided { get; private set; }
    public ItemModel? ItemToEdit { get; private set; }
    public bool ItemFailedToEdit { get; private set; }


    // Data from view 
    [BindProperty]
    [FromForm]
    public InputModel? Input { get; set; }

    public IActionResult OnGet([FromRoute] int? id)
    {
        if (id is int itemID)
        {
            ItemToEdit = service.GetItemByID(itemID);
            ItemIDFromRoute = itemID;
            return Page();
        }

        NoIDProvided = true;
        return Page();
    }

    public IActionResult OnPost([FromRoute] int? id)
    {
        if (id is int itemID)
        {
            ItemToEdit = service.GetItemByID(itemID);
            ItemIDFromRoute = itemID;
        }

        if (!ModelState.IsValid)
        {
            ItemFailedToEdit = true; 
            return Page();
        }

        if (Input is InputModel input)
        {
            bool edited = service.UpdateItemByID(input.Id, new ItemModel()
            {
                Id = input.Id,
                Name = input.Name,
                Description = input.Description ?? string.Empty, 
                Quantity = input.Quantity,
            });

            if (edited)
            {
                return RedirectToPage("Home"); 
            }
        }

        ItemFailedToEdit = true; 
        return Page();
    }



    public class InputModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Name can not be more than 20 characters long")]
        public required string Name { get; set; }

        [StringLength(50, ErrorMessage = "Description needs to be less than 50 characters long")]
        public string? Description { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity needs to be more than 1")]
        public int Quantity { get; set; }

    }
}
