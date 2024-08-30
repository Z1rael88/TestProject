using System.Net.Http.Json;
using ApiClient.Models;
using ApiClient.Options;

namespace ApiClient;

public class ApiClientService
{
    private readonly HttpClient _httpClient;
    public ApiClientService(ApiClientOptions apiClientOptions)
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new System.Uri(apiClientOptions.BaseAddress);
    }

    public async Task<List<Project>?> GetProjects()
    {
        return await _httpClient.GetFromJsonAsync<List<Project>?>("/api/Projects");
    }

    public async Task<Project?> GetById(Guid Id)
    {
        return await _httpClient.GetFromJsonAsync<Project?>($"/api/Projects/{Id}");
    }

    public async Task SaveProject(Project project)
    {
        await _httpClient.PostAsJsonAsync("/api/Projects",project);
    }

    public async Task UpdateProject(Project project)
    {
        await _httpClient.PutAsJsonAsync("/api/Projects", project);
    }

    public async Task DeleteProject(Guid Id)
    {
        await _httpClient.DeleteAsync($"/api/Projects/{Id}");
    }
}