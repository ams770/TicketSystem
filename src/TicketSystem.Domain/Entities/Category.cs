using TicketSystem.Domain.Exceptions;

namespace TicketSystem.Domain.Entities;

public class Category
{
    // EF Core needs.
    private Category()
    {
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;


    public static Category Create(string name)
    {
        ValidateName(name);

        return new Category
        {
            Id = Guid.NewGuid(),
            Name = name.Trim()
        };
    }


    public void UpdateName(string newName)
    {
        ValidateName(newName);
        Name = newName.Trim();
    }

    private static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Name is required.");

        if (name.Length > 40)
            throw new DomainException("Max name length is 40");
        
    }
}