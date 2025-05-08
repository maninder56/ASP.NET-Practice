using ShoppingList.Modesl;

namespace ShoppingList.Services; 

public interface IShoppingListService
{
    // Read 
    public List<ItemModel> GetAllItems();
    public ItemModel? GetItemByID(int id);

    // Create 
    public ItemModel? CreateItem(ItemModel newItem);

    // Update
    public bool UpdateItemByID(int id, ItemModel item);

    // Delete 
    public bool DeleteItemByID(int id); 
}
