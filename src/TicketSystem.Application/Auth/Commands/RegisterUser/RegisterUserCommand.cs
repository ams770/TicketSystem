namespace TicketSystem.Application.Auth.Commands.RegisterUser;

public class RegisterUserCommand
{
    public string FullName { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}