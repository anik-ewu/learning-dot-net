// using JobBoard.Application.DTOs.Auth;
using JobBoard.Application.Services.Auth;
using JobBoard.Domain.Entities;
using JobBoard.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace JobBoard.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly ITokenService _tokenService;

    public AuthController(AppDbContext db, ITokenService tokenService)
    {
        _db = db;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var existing = await _db.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
        if (existing != null) return BadRequest("User already exists");

        var hash = ComputeHash(request.Password);

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            PasswordHash = hash,
            Role = "User"
        };

        var refreshToken = _tokenService.GenerateRefreshToken();
        user.RefreshTokens.Add(refreshToken);

        _db.Users.Add(user);
        await _db.SaveChangesAsync();

        var accessToken = _tokenService.GenerateAccessToken(user);

        return Ok(new AuthResponse(accessToken, refreshToken.Token));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await _db.Users.Include(u => u.RefreshTokens)
                                  .FirstOrDefaultAsync(u => u.Email == request.Email);

        if (user == null || user.PasswordHash != ComputeHash(request.Password))
            return Unauthorized();

        var refreshToken = _tokenService.GenerateRefreshToken();
        user.RefreshTokens.Add(refreshToken);

        await _db.SaveChangesAsync();

        var accessToken = _tokenService.GenerateAccessToken(user);
        return Ok(new AuthResponse(accessToken, refreshToken.Token));
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(RefreshRequest request)
    {
        var user = await _db.Users
            .Include(u => u.RefreshTokens)
            .FirstOrDefaultAsync(u => u.RefreshTokens
                .Any(rt => rt.Token == request.RefreshToken));
        
        if (user is null)
            return Unauthorized("Invalid refresh token");

        var existing = user.RefreshTokens.First(rt => rt.Token == request.RefreshToken);

        if (existing.IsRevoked || existing.Expires < DateTime.UtcNow)
            return Unauthorized("Refresh token is expired or revoked");
        
        // Revoke old token
        existing.IsRevoked = true;

        // Generate new tokens
        var newRefresh = _tokenService.GenerateRefreshToken();
        user.RefreshTokens.Add(newRefresh);
        var newAccess = _tokenService.GenerateAccessToken(user);

        await _db.SaveChangesAsync();

        return Ok(new AuthResponse(newAccess, newRefresh.Token));
    }

    private string ComputeHash(string input)
    {
        using var sha = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(input);
        var hash = sha.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }
}
