using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingList.Models;
using ShoppingList.Services;

namespace ShoppingList.Pages
{
    public class ItemInfoModel : PageModel
    {
        private IShoppingListService service; 

        public ItemInfoModel(IShoppingListService service)
        {
            this.service = service;
        }


        // Data Shared with view 
        public ItemModel? ItemInfo { get; private set; }
        public int RequestedItemID { get; private set; }
        public bool NoIDProvided { get; private set; }

        public IActionResult OnGet([FromRoute] int? id)
        {
            if (id is int itemID)
            {
                ItemInfo = service.GetItemByID(itemID);
                RequestedItemID = itemID;
                return Page();
            }

            NoIDProvided = true;
            return Page();
        }
    }
}
