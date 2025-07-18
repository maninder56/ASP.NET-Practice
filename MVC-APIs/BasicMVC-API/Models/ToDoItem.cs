namespace BasicMVC_API.Models; 

public class ToDoItem
{
    public int id { get; set; }

    public string name { get; set; }

    public bool isCompleted { get; set; } = false;

    public ToDoItem(int id, string name)
    {
        this.id = id; this.name = name;
    }
}
