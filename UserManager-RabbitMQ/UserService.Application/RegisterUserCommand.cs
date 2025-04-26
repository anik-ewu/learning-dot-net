using MediatR;

namespace UserService.Application.Users.Commands;

public record RegisterUserCommand(string Name, string Email) : IRequest;
