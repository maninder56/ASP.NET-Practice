using ToDoApp.Model;

namespace ToDoApp.Services; 

public interface IToDoDataService
{
    // Read Operations 
    public List<TodoModel> GetAllToDoTasks(); 
    public TodoModel? GetToDoById(int id);   

    // Create Operations 
    public TodoModel? CreateTodoTask(TodoModel todoModel);

    // Update Operations 
    public bool UpdateToDoByID(int id, TodoModel todoModel);
    public bool UpdateToDoToCompletedByID(int id); 

    // Delete Operations 
    public bool DeleteToDoByID(int id); 
}
