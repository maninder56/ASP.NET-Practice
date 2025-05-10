using ShoppingList.Models;
using System.Collections.Concurrent;

namespace ShoppingList.Services;

public class ShoppingListService : IShoppingListService
{
    private ConcurrentDictionary<int, ItemModel> shoppingList; 

    public ShoppingListService(ShoppingListData shoppingListData)
    {
        shoppingList = shoppingListData.List;
    }

    // Read 
    public List<ItemModel> GetAllItems()
    {
        return shoppingList.Values.Any() ? 
            shoppingList.Values.ToList() : new List<ItemModel>();
    }

    public ItemModel? GetItemByID(int id)
    {
        shoppingList.TryGetValue(id, out ItemModel? item);
        return item; 
    }


    // Create 
    public ItemModel? CreateItem(ItemModel newItem)
    {
        newItem.Id = shoppingList.Keys.Max() + 1;

        return shoppingList.TryAdd(newItem.Id, newItem) ? newItem : null;
    }


    // Update
    public bool UpdateItemByID(int id, ItemModel item)
    {
        shoppingList.TryGetValue(id, out ItemModel? oldItem);

        if (oldItem is null)
        {
            return false;
        }

        return shoppingList.TryUpdate(id, item, oldItem);
    }


    // Remove
    public bool DeleteItemByID(int id)
    {
        return shoppingList.TryRemove(id, out ItemModel? item);
    }
}
