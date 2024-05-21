using ToDoList.Application;
using ToDoList.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddSession(); // Add session support
builder.Services.AddDistributedMemoryCache(); // Add in-memory cache
builder.Services.AddControllersWithViews(); // Add MVC services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddScoped<IToDoService, ToDoService>();
builder.Services.AddScoped<IToDoRepository, ToDoRepository>();
builder.Services.AddSingleton<IToDoRepository, ToDoRepository>();
builder.Services.AddScoped<IToDoService, ToDoService>();
// Add other service registrations

var app = builder.Build();

// Configure the middleware
app.UseSession();
app.UseStaticFiles();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
        name: "account",
        pattern: "Account/{action=Login}");
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


// You can add more routes for specific controllers if necessary
app.MapControllerRoute(
    name: "account",
    pattern: "{controller=Account}/{action=Login}");

app.UseDeveloperExceptionPage();

app.Run();