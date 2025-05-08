using ShoppingList.Modesl;
using System.Collections.Concurrent;

namespace ShoppingList.Services; 

public class ShoppingListData
{
    public ConcurrentDictionary<int, ItemModel> List; 

    public ShoppingListData()
    {
        List = new ConcurrentDictionary<int, ItemModel>();

        List.TryAdd(1, new ItemModel(1, "Watter bottel", 5));
        List.TryAdd(2, new ItemModel(2, "Fruit", "Apples, Banannas, and Pear", 3));
        List.TryAdd(3, new ItemModel(3,"Bread", 1));
        List.TryAdd(4, new ItemModel(4, "Butter", 1)); 
        List.TryAdd(5, new ItemModel(5, "Honey", 2));
        List.TryAdd(6, new ItemModel(6, "Fish", 5));
        List.TryAdd(7, new ItemModel(7, "Eggs", "Large size", 12)); 
    }
}
