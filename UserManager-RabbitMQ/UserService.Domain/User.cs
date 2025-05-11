using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UserService.Domain.Entities;

public class User
{   
    // Using global settings
    // [BsonId]
    // [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
}
