using ToDoWebApp.Models;

namespace ToDoWebApp.Services; 

public interface IToDoService
{
    public List<ToDoItem> GetAllToDoItems(); 

    public bool CreateToDoItem(string name);

    public bool SetToDoCompletedByID(int id);   

    public bool DeleteToDoByID(int id);
}
