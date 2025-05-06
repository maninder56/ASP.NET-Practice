using System.Collections.Concurrent;
using ToDoApp.Model;

namespace ToDoApp.ToDoData; 

public class ToDoConcurrentDictionary
{
    public ConcurrentDictionary<int, TodoModel> ToDoDictionary; 

    public ToDoConcurrentDictionary()
    {
        ToDoDictionary = new ConcurrentDictionary<int, TodoModel>();

        ToDoDictionary.TryAdd(1, new TodoModel(1, "Check Emails")); 
        ToDoDictionary.TryAdd(2, new TodoModel(2, "Delete Temporary Files")); 
        ToDoDictionary.TryAdd(3, new TodoModel(3, "Finish HomeWork") { Description = "English: Essay and Maths: Quadratic Equations"}); 
        ToDoDictionary.TryAdd(4, new TodoModel(4, "Buy Eggs")); 
        ToDoDictionary.TryAdd(5, new TodoModel(5, "Dust all the books")); 
    }
}
