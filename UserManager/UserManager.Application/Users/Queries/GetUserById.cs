using MediatR;
using UserManager.Application.Users.DTOs;

namespace UserManager.Application.Users.Queries;

public class GetUserByIdQuery : IRequest<UserDto>
{
    public Guid Id { get; set; }

    public GetUserByIdQuery(Guid id)
    {
        Id = id;
    }
}
