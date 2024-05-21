using Microsoft.AspNetCore.Mvc;
using ToDoList.Application;
using ToDoList.Domain;
using ToDoList.Models;
using System.Threading.Tasks;

public class AccountController : Controller
{
    private readonly IUserService _userService;

    public AccountController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public IActionResult Register()
    {
        // Return the registration view
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Check if the password and confirm password match
            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError(string.Empty, "Passwords do not match.");
                return View(model);
            }

            // Check if the username already exists
            var existingUser = await _userService.GetUserByUsername(model.Username);
            if (existingUser != null)
            {
                ModelState.AddModelError(string.Empty, "Username is already taken.");
                return View(model);
            }

            // Register the new user
            var newUser = new User
            {
                Username = model.Username,
                Password = model.Password
            };

            await _userService.Register(newUser);

            // Redirect to the login page after successful registration
            return RedirectToAction("Login");
        }

        // Return the registration view with model errors if any
        return View(model);
    }

    [HttpGet]
    public IActionResult Login()
    {
        // Return the login view
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Authenticate the user with the provided username and password
            var user = await _userService.AuthenticateAsync(model.Username, model.Password);
            if (user != null)
            {
                // Store the user ID in the session to maintain login state
                HttpContext.Session.SetInt32("UserId", user.UserId);

                // Redirect the user to the dashboard (Home/Index)
                return RedirectToAction("Index", "Home");
            }

            // If authentication fails, add an error message
            ModelState.AddModelError(string.Empty, "Invalid username or password.");
        }

        // Return the login view with model errors if authentication fails
        return View(model);
    }

    [HttpPost]
    public IActionResult Logout()
    {
        // Clear the user's session
        HttpContext.Session.Clear();

        // Redirect the user to the login page
        return RedirectToAction("Login");
    }
}
