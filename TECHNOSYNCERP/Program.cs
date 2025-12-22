using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TECHNOSYNCERP.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new SessionCheckAttribute());
});

builder.Services.AddDistributedMemoryCache(); // required for session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(3);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession(); // Session middleware should be after Routing
app.UseMiddleware<SessionValidationMiddleware>();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

public class SessionCheckAttribute : ActionFilterAttribute
{
    private readonly List<string> _allowedActions = new List<string>
    {
        "Home/Index",
    };

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var controller = context.RouteData.Values["controller"]?.ToString();
        var action = context.RouteData.Values["action"]?.ToString();
        var currentAction = $"{controller}/{action}";

        // Skip session check for allowed actions
        if (_allowedActions.Contains(currentAction))
        {
            base.OnActionExecuting(context);
            return;
        }

        var userId = context.HttpContext.Session.GetString("UserID");

        if (string.IsNullOrEmpty(userId))
        {
            // Set session expired flag
            context.HttpContext.Items["SessionExpired"] = true;

            // Redirect to Home/Index with session expired flag
            context.Result = new RedirectToActionResult("Index", "Home", new { sessionExpired = true });
            return;
        }

        base.OnActionExecuting(context);
    }
}