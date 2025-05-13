using KafkaUserDemo.Consumer;
using KafkaUserDemo.Consumer.Data;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<MongoDbContext>(); // ✅ Add this line
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();

