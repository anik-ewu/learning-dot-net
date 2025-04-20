using MediatR;
using UserManager.Application.Users.DTOs;
using UserManager.Application.Users.Queries;
using UserManager.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace UserManager.Application.Users.Handlers
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        public Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = CreateUserCommandHandler._db.FirstOrDefault(u => u.Id == request.Id);

            if (user == null) return Task.FromResult<UserDto>(null);

            var dto = new UserDto
            {
                Id = user.Id,
                Name = user.Name
            };

            return Task.FromResult(dto);
        }
    }
}
