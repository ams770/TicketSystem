
using TicketSystem.Application.Categories.Queries.Dtos;
using TicketSystem.Domain.Interfaces;

namespace TicketSystem.Application.Categories.Queries.GetAllCategories;

public class GetAllCategoriesService(ICategoryRepo categoryRepo)
{
    public async Task<ICollection<CategoryDto>> ExecuteAsync()
    {
        var categories = await categoryRepo.GetAllAsync();
        // Map to Dto 
        var dtoItems = categories
            .Select(item => item.ToDto())
            .ToList();

        return dtoItems;
    }
}