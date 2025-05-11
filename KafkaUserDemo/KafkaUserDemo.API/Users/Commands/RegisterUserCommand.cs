using KafkaUserDemo.Domain.Entities;
using MediatR;

namespace KafkaUserDemo.API.Users.Commands;

public class RegisterUserCommand : IRequest
{
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
}
