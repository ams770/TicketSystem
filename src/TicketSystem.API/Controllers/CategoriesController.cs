using Microsoft.AspNetCore.Mvc;

namespace TicketSystem.API.Controllers;

public class CategoriesController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}