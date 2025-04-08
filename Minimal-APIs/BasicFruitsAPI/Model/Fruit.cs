using System.ComponentModel.DataAnnotations; 

namespace BasicFruitsAPI.Model; 

public class Fruit
{
    [Required]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [StringLength(100)]
    public string? Classification { get; set; }

    public string? Description { get; set; }

    public Fruit () { }

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
