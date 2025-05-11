using Confluent.Kafka;
using MediatR;
using System.Text.Json;
using KafkaUserDemo.Domain.Entities;

namespace KafkaUserDemo.API.Users.Commands;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand>
{
    public async Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var config = new ProducerConfig { BootstrapServers = "localhost:9092" };

        using var producer = new ProducerBuilder<Null, string>(config).Build();

        var user = new UserItem
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Email = request.Email
        };

        var message = JsonSerializer.Serialize(user);

        await producer.ProduceAsync("user-registered", new Message<Null, string> { Value = message });

        Console.WriteLine("Message published to Kafka.");
    }
}
