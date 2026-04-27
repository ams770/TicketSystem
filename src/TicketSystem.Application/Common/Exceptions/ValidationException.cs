namespace TicketSystem.Application.Common.Exceptions;

public class ValidationException(string message) : Exception(message);