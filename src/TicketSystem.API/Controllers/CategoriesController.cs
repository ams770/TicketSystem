
using Microsoft.AspNetCore.Mvc;
using TicketSystem.Application.Categories.Queries.GetAllCategories;

namespace TicketSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController(GetAllCategoriesService categoriesService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
        var result = await categoriesService.ExecuteAsync();
        return Ok(result);
    }
}