using JobBoard.Domain.Entities;

namespace JobBoard.Application.Services.Auth;

public interface ITokenService
{
    string GenerateAccessToken(User user);
    RefreshToken GenerateRefreshToken();
}
