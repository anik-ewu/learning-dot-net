using MediatR;

namespace UserManager.Application.Users.Commands
{
    public class CreateUserCommand : IRequest<Guid>
    {
        public string Name { get; set; }

        public CreateUserCommand(string name)
        {
            Name = name;
        }
    }
}

