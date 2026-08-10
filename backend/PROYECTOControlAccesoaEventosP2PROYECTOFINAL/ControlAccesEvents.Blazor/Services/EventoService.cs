using System.Net.Http.Json;
using ControlAccesEvents.Blazor.DTOs;

namespace ControlAccesEvents.Blazor.Services;

public class EventoService
{
    private readonly IHttpClientFactory _factory;
    private readonly string _route = "api/eventos";
    public string? LastError { get; private set; }

    public EventoService(IHttpClientFactory factory)
    {
        _factory = factory;
    }

    public async Task<List<EventoDto>?> GetAllAsync()
    {
        try
        {
            var client = _factory.CreateClient("ApiClient");
            var result = await client.GetFromJsonAsync<List<EventoDto>>(_route);
            LastError = null;
            return result;
        }
        catch
        {
            LastError = "No se pudo conectar con la API. Verifique que el backend esté ejecutándose.";
            return null;
        }
    }

    public async Task<EventoDto?> GetByIdAsync(int id)
    {
        try
        {
            var client = _factory.CreateClient("ApiClient");
            var result = await client.GetFromJsonAsync<EventoDto>($"{_route}/{id}");
            LastError = null;
            return result;
        }
        catch
        {
            LastError = "No se pudo conectar con la API. Verifique que el backend esté ejecutándose.";
            return null;
        }
    }

    public async Task<bool> CreateAsync(EventoDto dto)
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

    public async Task<bool> UpdateAsync(int id, EventoDto dto)
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
