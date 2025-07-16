using ToDoWebApp.Data;
using ToDoWebApp.Models;

namespace ToDoWebApp.Services;

public class ToDoService : IToDoService
{
    private ILogger<ToDoService> logger;

    private ToDoData ToDoData;

    public ToDoService(ILogger<ToDoService> logger, ToDoData data)
    {
        this.logger = logger;
        ToDoData = data;
    }


    public List<ToDoItem> GetAllToDoItems()
    {
        logger.LogInformation("Requested all ToDo in list"); 

        return ToDoData.ToDoList; 
    }

    public bool CreateToDoItem(string name)
    {
        logger.LogInformation("Requested to add ToDo with {ToDoTitle}", name); 

        int id = ToDoData.ToDoList.Max(i => i.id) + 1;

        ToDoData.ToDoList.Add(new ToDoItem(id, name));

        return true;
    }


    public bool SetToDoCompletedByID(int id)
    {
        logger.LogInformation("Requested to mark ToDo completed by ID {ID}", id);

        ToDoItem? item = ToDoData.ToDoList.Where(i => i.id == id).FirstOrDefault(); 

        if (item is null)
        {
            logger.LogWarning("Item does not exists with id {ID}", id); 
            return false; 
        }

        item.isCompleted = true;

        return true; 
    }

    public bool DeleteToDoByID(int id)
    {
        logger.LogInformation("Requested to delete ToDo by ID {ID}", id);

        ToDoItem? item = ToDoData.ToDoList.Where(i => i.id == id).FirstOrDefault(); 

        if (item is null)
        {
            logger.LogWarning("Item does not exists with id {ID}", id);
            return false;
        }

        return ToDoData.ToDoList.Remove(item);

    }

}
