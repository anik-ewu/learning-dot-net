using Confluent.Kafka;
using KafkaUserDemo.Consumer.Data;
using KafkaUserDemo.Domain.Entities;
using System.Text.Json;

namespace KafkaUserDemo.Consumer;

public class Worker : BackgroundService
{
    private readonly MongoDbContext _mongoDb = new();

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = "localhost:9092",
            GroupId = "user-consumer-group",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();

        consumer.Subscribe("user-registered");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var result = consumer.Consume(stoppingToken);
                var user = JsonSerializer.Deserialize<UserItem>(result.Message.Value);

                if (user != null)
                {
                    await _mongoDb.Users.InsertOneAsync(user);
                    Console.WriteLine($"[Kafka] Saved user to MongoDB: {user.Name}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Kafka Error] {ex.Message}");
            }
        }
    }
}
