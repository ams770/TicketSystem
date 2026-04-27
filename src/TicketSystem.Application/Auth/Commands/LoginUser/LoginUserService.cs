using TicketSystem.Application.Common.Exceptions;
using TicketSystem.Application.Interfaces;
using TicketSystem.Domain.Entities;
using TicketSystem.Domain.Interfaces;

namespace TicketSystem.Application.Auth.Commands.LoginUser;

public class LoginUserService(IUserRepo userRepo, IPasswordHasher passwordHasher, IJwtService jwtService)
{
    public async Task<LoginUserResult> ExecuteAsync(LoginUserCommand command)
    {
        // Get User
        var existUser = await userRepo.GetByUsernameAsync(command.Username);
        // Check if the username exists & the password is valid
        var isAuthenticated =
            existUser is not null && passwordHasher.Verify(command.Password, existUser.PasswordHash);
        // Handle wrong authentication
        if (!isAuthenticated) throw new UnauthorizedException("Username or password is incorrect");
        // Generate token
        var accessToken = jwtService.GenerateJwtToken(existUser!.Id, existUser.Username, "User");
        return new LoginUserResult
        {
            AccessToken = accessToken,
        };
    }
}