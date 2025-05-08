namespace ShoppingList.Modesl; 

public class ItemModel
{
    public int Id { get; set; }

    public string Name { get; set; } = null!; 

    public string Description { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public ItemModel () { }

    public ItemModel(int id, string name, int quantity)
    {
        Id = id;
        Name = name;
        Quantity = quantity;
    }

    public ItemModel(int id, string name, string description, int quantity)
    {
        Id = id;
        Name = name;
        Description = description;
        Quantity = quantity;
    }

    
}
