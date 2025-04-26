using MongoDB.Driver;
using UserService.Domain.Entities;

namespace UserService.Infrastructure.Repositories;

public class UserRepository
{
    private readonly IMongoCollection<User> _collection;

    public UserRepository()
    {
        var client = new MongoClient("mongodb+srv://Cluster79073:c1JjaU9eYGJj@cluster0.mongodb.net/UserManager-RabbitMQ?retryWrites=true&w=majority");
        var database = client.GetDatabase("UserDb");
        _collection = database.GetCollection<User>("Users");
    }

    public async Task AddUserAsync(User user)
    {
        await _collection.InsertOneAsync(user);
    }
}
