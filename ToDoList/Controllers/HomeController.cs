using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using ToDoList.Application;
using ToDoList.Domain;
using ToDoList.Models;

public class HomeController : Controller
{
    private readonly IToDoService _toDoService;

    public HomeController(IToDoService toDoService)
    {
        _toDoService = toDoService;
    }

    public async Task<IActionResult> Index()
    {
        int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
        if (userId == 0)
        {
            return RedirectToAction("Login", "Account");
        }

        var pendingTasks = await _toDoService.GetPendingToDoItemsByUserId(userId);
        var completedTasks = await _toDoService.GetCompletedToDoItemsByUserId(userId);

        var viewModel = new DashboardViewModel
        {
            PendingToDoItems = pendingTasks,
            DoneToDoItems = completedTasks,
            Username = "User", // Replace with actual username retrieval logic
            PendingCount = pendingTasks.Count, // Update pending tasks count
            DoneCount = completedTasks.Count // Update completed tasks count
        };

        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
        {
            return PartialView("_CompletedTasks", viewModel.DoneToDoItems);
        }

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Add(ToDoItemViewModel model)
    {
        if (ModelState.IsValid)
        {
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
            if (userId == 0)
            {
                return RedirectToAction("Login", "Account");
            }

            var newItem = new ToDoItem
            {
                Title = model.Title,
                Description = model.Description,
                DueDate = model.DueDate,
                UserId = userId
            };

            await _toDoService.AddToDoItem(newItem);

            // Reload pending tasks and update counts
            var pendingTasks = await _toDoService.GetPendingToDoItemsByUserId(userId);
            var completedTasks = await _toDoService.GetCompletedToDoItemsByUserId(userId);

            var viewModel = new DashboardViewModel
            {
                PendingToDoItems = pendingTasks,
                DoneToDoItems = completedTasks,
                Username = "User", // Replace with actual username retrieval logic
                PendingCount = pendingTasks.Count, // Update pending tasks count
                DoneCount = completedTasks.Count // Update completed tasks count
            };

            // Return the updated view
            return View("Index", viewModel);
        }

        // If model is not valid, return to the dashboard with errors
        return RedirectToAction("Index", model);
    }
    [HttpPost]
    public async Task<IActionResult> MarkDone(int id)
    {
        // Get the task
        var task = await _toDoService.GetById(id);

        if (task != null)
        {
            task.IsDone = true;
            task.CompletedDate = DateTime.Now;
            await _toDoService.Update(task);
        }
        return RedirectToAction("Index");
    }


}
