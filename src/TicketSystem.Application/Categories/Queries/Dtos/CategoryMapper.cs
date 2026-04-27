using TicketSystem.Domain.Entities;

namespace TicketSystem.Application.Categories.Queries.Dtos;

public static class CategoryMapper
{
    public static CategoryDto ToDto(this Category category) => new()
    {
        Id = category.Id,
        Name = category.Name
    };
}