namespace LoggingErrors.Models; 

public class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public Product () { }

    public Product (int id, string name, string category)
    {
        Id = id;
        Name = name;
        Category = category;
    }
}
