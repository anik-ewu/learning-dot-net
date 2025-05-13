using KafkaUserDemo.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace KafkaUserDemo.Consumer.Data;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext()
    {
        var settings = MongoClientSettings.FromConnectionString("mongodb+srv://Cluster79073:7qdavcpoK79G9YUY@cluster79073.ick23.mongodb.net/UserManager-RabbitMQ?retryWrites=true&w=majority");
        settings.GuidRepresentation = GuidRepresentation.Standard;
        
        var client = new MongoClient(settings);
        _database = client.GetDatabase("UserManager-RabbitMQ");
    }

    public IMongoCollection<UserItem> Users => _database.GetCollection<UserItem>("UserList");
}
