using TicketSystem.Application.Common.Exceptions;
using TicketSystem.Application.Interfaces;
using TicketSystem.Domain.Entities;
using TicketSystem.Domain.Interfaces;

namespace TicketSystem.Application.Auth.Commands.LoginAgent;

public class LoginAgentService(IAgentRepo agentRepo, IPasswordHasher passwordHasher, IJwtService jwtService)
{
    public async Task<LoginAgentResult> ExecuteAsync(LoginAgentCommand command)
    {
        // Get Agent
        var existAgent = await agentRepo.GetByUsernameAsync(command.Username);
        // Check if the user exists & the password is valid
        var isAuthenticated = existAgent != null  && passwordHasher.Verify(command.Password, existAgent.PasswordHash);
        // Handle wrong authentication
        if (isAuthenticated) throw new UnauthorizedException("Username or password is incorrect");
        // Generate token
        var accessToken = jwtService.GenerateJwtToken(existAgent!.Id, existAgent.Username, "Agent");
        return new LoginAgentResult
        {
            AccessToken = accessToken,
        };
    }
}