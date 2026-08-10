using System.Net.Http.Json;
using ControlAccesEvents.Blazor.DTOs;

namespace ControlAccesEvents.Blazor.Services;

public class ValidacionService
{
    private readonly IHttpClientFactory _factory;
    private readonly string _route = "api/validaciones";
    public string? LastError { get; private set; }

    public ValidacionService(IHttpClientFactory factory)
    {
        _factory = factory ?? throw new ArgumentNullException(nameof(factory));
    }

    public async Task<List<ValidacionDto>?> GetAllAsync()
    {
        try
        {
            var client = _factory.CreateClient("ApiClient");
            var result = await client.GetFromJsonAsync<List<ValidacionDto>>(_route);
            LastError = null;
            return result;
        }
        catch
        {
            LastError = "No se pudo conectar con la API. Verifique que el backend esté ejecutándose.";
            return null;
        }
    }

    public async Task<ValidacionDto?> GetByIdAsync(int id)
    {
        try
        {
            var client = _factory.CreateClient("ApiClient");
            var result = await client.GetFromJsonAsync<ValidacionDto>($"{_route}/{id}");
            LastError = null;
            return result;
        }
        catch
        {
            LastError = "No se pudo conectar con la API. Verifique que el backend esté ejecutándose.";
            return null;
        }
    }

    public async Task<bool> CreateAsync(ValidacionDto dto)
    {
        try
        {
            var client = _factory.CreateClient("ApiClient");
            var resp = await client.PostAsJsonAsync(_route, dto);
            LastError = null;
            return resp.IsSuccessStatusCode;
        }
        catch
        {
            LastError = "No se pudo conectar con la API. Verifique que el backend esté ejecutándose.";
            return false;
        }
    }

    public async Task<bool> UpdateAsync(int id, ValidacionDto dto)
    {
        try
        {
            var client = _factory.CreateClient("ApiClient");
            var resp = await client.PutAsJsonAsync($"{_route}/{id}", dto);
            LastError = null;
            return resp.IsSuccessStatusCode;
        }
        catch
        {
            LastError = "No se pudo conectar con la API. Verifique que el backend esté ejecutándose.";
            return false;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var client = _factory.CreateClient("ApiClient");
            var resp = await client.DeleteAsync($"{_route}/{id}");
            LastError = null;
            return resp.IsSuccessStatusCode;
        }
        catch
        {
            LastError = "No se pudo conectar con la API. Verifique que el backend esté ejecutándose.";
            return false;
        }
    }
}
