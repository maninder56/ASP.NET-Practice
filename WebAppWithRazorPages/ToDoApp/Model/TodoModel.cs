using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Model; 

public class TodoModel
{
    [Required]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    public DateTime CreatedDate { get; set; }

    public bool isCompleted { get; set; }

    public TodoModel(int id, string name)
    {
        Id = id;
        Name = name; 
        CreatedDate = DateTime.Now; 
        isCompleted = false;
    }

    public TodoModel() { }

    public void SetTodoCompleted() => isCompleted = true;

    public void SetID(int id) => Id = id;
}
