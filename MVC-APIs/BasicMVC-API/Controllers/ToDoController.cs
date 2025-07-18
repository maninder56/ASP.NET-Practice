using BasicMVC_API.Models;
using BasicMVC_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BasicMVC_API.Controllers; 

[Route("api/[controller]")]
[ApiController]
public class ToDoController : ControllerBase
{
    private ILogger<ToDoController> logger;

    private IToDoService service; 

    public record ToDoName(string name); 

    public ToDoController(ILogger<ToDoController> logger, IToDoService toDoService)
    {
        this.logger = logger;
        this.service = toDoService;
    }

    [Route("")]
    [HttpGet("list")]
    public ActionResult<List<ToDoItem>> GetAllToDoItems()
    {
        List<ToDoItem> list = service.GetAllToDoItems();

        if (list.Count == 0)
        {
            return NoContent(); 
        }

        return list; 
    }


    [HttpPost("add")]
    public ActionResult<ToDoItem> AddNewTodo([FromBody] ToDoName name)
    {
        bool created = service.CreateToDoItem(name.name);

        if (!created)
        {
            return BadRequest();
        }

        return Ok();
    }



    [HttpPut("completed/{id}")]
    public ActionResult<ToDoItem> MarkTodoCompletedByID(int id)
    {
        bool changed = service.SetToDoCompletedByID(id);

        if (!changed)
        {
            return NotFound();
        }

        ToDoItem? item = service.GetToDoItemByID(id);

        if (item is null)
        {
            return NoContent(); 
        }

        return item;
    }


    [HttpDelete("delete/{id}")]
    public ActionResult DeleteToDoByID(int id)
    {
        bool deleted = service.DeleteToDoByID(id);

        if (!deleted)
        {
            return NotFound();
        }

        return Ok();
    }


}
