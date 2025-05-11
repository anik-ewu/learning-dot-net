using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson;
using MongoDB.Driver;
using UserService.Domain.Entities;

namespace UserService.Infrastructure.Repositories;

public class UserRepository
{
    private readonly IMongoCollection<User> _collection;

    static UserRepository()
    {
        // Register the Guid serializer globally ONCE
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
    }

    public UserRepository()
    {
        var client = new MongoClient("mongodb+srv://Cluster79073:7qdavcpoK79G9YUY@cluster79073.ick23.mongodb.net/UserManager-RabbitMQ?retryWrites=true&w=majority");
        var database = client.GetDatabase("UserManager-RabbitMQ ");
        _collection = database.GetCollection<User>("Users");
    }

    public async Task AddUserAsync(User user)
    {
        await _collection.InsertOneAsync(user);
    }
}
