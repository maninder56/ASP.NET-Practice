using System.Collections.Concurrent;
using ToDoApp.Model;
using ToDoApp.ToDoData;

namespace ToDoApp.Services;

public class ToDoDataService : IToDoDataService
{
    private ConcurrentDictionary<int, TodoModel> ToDoData; 
    
    public ToDoDataService(ToDoConcurrentDictionary toDoData)
    {
        ToDoData = toDoData.ToDoDictionary;
    }

    // Read Operations 
    public List<TodoModel> GetAllToDoTasks()
    {
        return ToDoData.Values.ToList() ?? new List<TodoModel>();
    }

    public TodoModel? GetToDoById(int id)
    {
        ToDoData.TryGetValue(id, out TodoModel? toDoModel);

        return toDoModel;
    }


    // Create Operations
    public TodoModel? CreateTodoTask(TodoModel todoModel)
    {
        todoModel.SetID( ToDoData.Keys.Max() + 1);

        return ToDoData.TryAdd(todoModel.Id, todoModel) ? todoModel : null;    
    }


    // Update Operations
    public bool UpdateToDoByID(int id, TodoModel NewTodoModel)
    {
        TodoModel? oldTodo = GetToDoById(id) ?? new TodoModel(0,"Null");

        return ToDoData.TryUpdate(id, NewTodoModel, oldTodo); 
    }

    public bool UpdateToDoToCompletedByID(int id)
    {
        TodoModel? todoModle = ToDoData.Values.FirstOrDefault(t => t.Id == id); 

        if (todoModle == null)
        {
            return false;
        }

        todoModle.SetTodoCompleted(); 
        return true;
    }

    // Delete Operations
    public bool DeleteToDoByID(int id)
    {
        return ToDoData.TryRemove(id, out _);
    }

    
}
