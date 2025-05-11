namespace KafkaUserDemo.Domain.Entities;

public class UserItem
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
}
