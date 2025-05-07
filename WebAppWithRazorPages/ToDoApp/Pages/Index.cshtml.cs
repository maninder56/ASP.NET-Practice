using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDoApp.Model;
using ToDoApp.Services;

namespace ToDoApp.Pages;

public class IndexModel : PageModel
{
    private readonly IToDoDataService service; 

    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger, IToDoDataService service)
    {
        _logger = logger;
        this.service = service;
    }

    // use Url.Page() method to generate links 

    public IActionResult OnGet()
    {
        TodoList = service.GetAllToDoTasks();
        return Page();
    }

    public IActionResult OnPostToDoCompleted(int toDoID)
    {
        service.UpdateToDoToCompletedByID(toDoID);
        return RedirectToPage("Index");
    }

    // Data shared with razor view 
    public List<TodoModel> TodoList { get; private set; } = new List<TodoModel>();
}
