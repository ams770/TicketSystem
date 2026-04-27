using TicketSystem.Application.Common.Exceptions;
using TicketSystem.Application.Interfaces;
using TicketSystem.Domain.Entities;
using TicketSystem.Domain.Interfaces;

namespace TicketSystem.Application.Auth.Commands.RegisterAgent;

public class RegisterAgentService(IAgentRepo agentRepo, IPasswordHasher passwordHasher, IJwtService jwtService)
{
    public async Task<RegisterAgentResult> ExecuteAsync(RegisterAgentCommand command)
    {
        // Check Duplications
        var existUsername = await agentRepo.GetByUsernameAsync(command.Username);
        if (existUsername is not null) throw new ConflictException("Username is already used");
        // Hash Password
        var hashedPassword = passwordHasher.HashPassword(command.Password);
        // Save Agent
        var newAgent = Agent.Create(command.FullName, command.Username, hashedPassword);
        await agentRepo.AddAsync(newAgent);
        await agentRepo.SaveChangesAsync();
        // Generate token
        var accessToken = jwtService.GenerateJwtToken(newAgent.Id, newAgent.Username, "Agent");
        return new RegisterAgentResult
        {
            AccessToken = accessToken,
        };
    }
}