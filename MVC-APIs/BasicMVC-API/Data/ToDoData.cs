using BasicMVC_API.Models;

namespace BasicMVC_API.Data; 

public class ToDoData
{
    public List<ToDoItem> ToDoList { get; set; } = new List<ToDoItem>()
    {
        new ToDoItem(1, "Finish the to do app")
    };
}
