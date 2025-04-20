using MediatR;
using UserManager.Application.Users.Commands; // Ensure this is added to reference CreateUserCommand
using UserManager.Domain.Entities;

namespace UserManager.Application.Users.Handlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        // Simulating in-memory DB
        public static List<User> _db = new();

        public Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User(request.Name);
            _db.Add(user);
            return Task.FromResult(user.Id);
        }
    }
}
