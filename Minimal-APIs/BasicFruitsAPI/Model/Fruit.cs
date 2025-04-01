namespace BasicFruitsAPI.Model; 

public class Fruit
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Classification { get; set; }
    public string? Description { get; set; }

    public Fruit(int id, string name)
    {
        Id = id; Name = name;
    }

    public Fruit(int id, string name, string classification, string description)
    {
        Id = id;
        Name = name;
        Classification = classification;
        Description = description;
    }
}
