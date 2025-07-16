using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ToDoWebApp.Models;
using ToDoWebApp.Services;

namespace ToDoWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;

        private IToDoService todos; 

        public HomeController(ILogger<HomeController> logger, IToDoService toDoService)
        {
            this.logger = logger;
            this.todos = toDoService;
        }

        public IActionResult Index()
        {
            logger.LogInformation("Index page requested"); 

            List<ToDoItem> items = todos.GetAllToDoItems();

            return View(items);
        }


        public IActionResult ToDoCompleted(int id)
        {
            logger.LogInformation("Requested to mark To Do completed by ID {ID}", id); 

            todos.SetToDoCompletedByID(id);

            return RedirectToAction("Index");
        }


        public IActionResult ToDoDelete(int id)
        {
            logger.LogInformation("Requested to delete To Do completed by ID {ID}", id);

            todos.DeleteToDoByID(id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddNewTodo([Bind] string todotitle)
        {
            todos.CreateToDoItem(todotitle);
            return RedirectToAction("Index"); 
        }

        public IActionResult Privacy()
        {
            logger.LogInformation("Privacy Page requested"); 
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
