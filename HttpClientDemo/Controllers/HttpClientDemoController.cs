using HttpClientDemo.Models;
using HttpClientDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace HttpClientDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HttpClientDemoController : ControllerBase
{
    private readonly HttpService _httpService;

    public HttpClientDemoController(HttpService httpService)
    {
        _httpService = httpService;
    }

    [HttpGet("users")]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _httpService.GetUsersAsync();
        return Ok(users);
    }

    [HttpPost("users")]
    public async Task<IActionResult> PostUser(User user)
    {
        var response = await _httpService.CreateUserAsync(user);
        return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
    }
}