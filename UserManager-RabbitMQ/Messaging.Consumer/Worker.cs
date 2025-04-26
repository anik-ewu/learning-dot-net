using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using UserService.Domain.Entities;
using UserService.Infrastructure.Repositories;

namespace Messaging.Consumer;

public class Worker : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: "user-registered", durable: false, exclusive: false, autoDelete: false, arguments: null);

        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            var user = JsonSerializer.Deserialize<User>(message);

            if (user != null)
            {
                var repository = new UserRepository();
                await repository.AddUserAsync(user);

                Console.WriteLine($"User saved: {user.Name} - {user.Email}");
            }
        };

        channel.BasicConsume(queue: "user-registered", autoAck: true, consumer: consumer);

        await Task.Delay(-1, stoppingToken); // Keep alive
    }
}
