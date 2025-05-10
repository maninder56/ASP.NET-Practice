using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingList.Models;
using ShoppingList.Services;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace ShoppingList.Pages
{
    public class AddItemModel : PageModel
    {
        public IShoppingListService service; 

        public AddItemModel(IShoppingListService service)
        {
            this.service = service;
        }

        // Data from view 
        [BindProperty]
        [FromForm]
        public InputModel? Input { get; set; }  

        // Data for view 
        public bool? ItemFailedToSaved { get; private set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                ItemFailedToSaved = true; 
                return Page(); 
            }

            if (Input is InputModel input)
            {
                var saved = service.CreateItem(new ItemModel()
                {
                    Id = 0,
                    Name = input.Name,
                    Description = input.Description ?? string.Empty,
                    Quantity = input.Quantity,
                });

                if (saved is not null)
                {
                    ItemFailedToSaved = false;
                    return RedirectToPage("Home");
                }
            }

            ItemFailedToSaved = true;
            return Page();
        }

        public class InputModel
        {
            [Required]
            [StringLength(50, ErrorMessage = "Name can not be more than 50 characters long")]
            public required string Name { get; set; }

            [StringLength(100)]
            public string? Description { get; set; }

            [Required]
            [Range(1, int.MaxValue)]
            public int Quantity { get; set; }
        }
    }
}
