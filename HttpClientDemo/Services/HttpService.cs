using HttpClientDemo.Models;
using System.Net.Http.Json;

namespace HttpClientDemo.Services;

public class HttpService
{
    private readonly HttpClient _httpClient;

    public HttpService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<User>> GetUsersAsync()
    {
        var users = await _httpClient.GetFromJsonAsync<List<User>>("https://jsonplaceholder.typicode.com/users");
        return users!;
    }

    public async Task<HttpResponseMessage> CreateUserAsync(User user)
    {
        var response = await _httpClient.PostAsJsonAsync("https://jsonplaceholder.typicode.com/users", user);
        return response;
    }
}