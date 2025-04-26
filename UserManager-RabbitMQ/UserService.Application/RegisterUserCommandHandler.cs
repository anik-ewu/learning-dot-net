using MediatR;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using UserService.Domain.Entities;

namespace UserService.Application.Users.Commands;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand>
{
    public async Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        
        channel.QueueDeclare(queue: "user-registered", durable: false, exclusive: false, autoDelete: false, arguments: null);

        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Email = request.Email
        };

        var message = JsonSerializer.Serialize(user);
        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(exchange: "", routingKey: "user-registered", basicProperties: null, body: body);
        
        await Task.CompletedTask;
    }
}
