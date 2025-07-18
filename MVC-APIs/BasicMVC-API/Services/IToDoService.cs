using BasicMVC_API.Models;

namespace BasicMVC_API.Services; 

public interface IToDoService
{
    public List<ToDoItem> GetAllToDoItems();

    public ToDoItem? GetToDoItemByID(int id); 

    public bool CreateToDoItem(string name);

    public bool SetToDoCompletedByID(int id);

    public bool DeleteToDoByID(int id);
}
