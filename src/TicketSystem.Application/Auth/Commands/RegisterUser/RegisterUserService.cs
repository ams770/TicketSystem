using TicketSystem.Application.Common.Exceptions;
using TicketSystem.Application.Interfaces;
using TicketSystem.Domain.Entities;
using TicketSystem.Domain.Interfaces;

namespace TicketSystem.Application.Auth.Commands.RegisterUser;

public class RegisterUserService(IUserRepo userRepo, IPasswordHasher passwordHasher, IJwtService jwtService)
{
    public async Task<RegisterUserResult> ExecuteAsync(RegisterUserCommand command)
    {
        // Check Duplications
        var existUsername = await userRepo.GetByUsernameAsync(command.Username);
        if (existUsername is not null) throw new ConflictException("Username is already used");
        // Hash Password
        var hashedPassword = passwordHasher.HashPassword(command.Password);
        // Save User
        var newUser = User.Create(command.FullName, command.Username, hashedPassword);
        await userRepo.AddAsync(newUser);
        await userRepo.SaveChangesAsync();
        // Generate token
        var accessToken = jwtService.GenerateJwtToken(newUser.Id, newUser.Username, "User");
        return new RegisterUserResult
        {
            AccessToken = accessToken,
        };
    }
}