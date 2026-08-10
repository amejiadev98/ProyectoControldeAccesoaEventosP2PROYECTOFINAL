using System.Net.Http.Json;
using ControlAccesEvents.Blazor.DTOs;

namespace ControlAccesEvents.Blazor.Services;

public class InvitadoService
{
    private readonly IHttpClientFactory _factory;
    private readonly string _route = "api/invitados";
    public string? LastError { get; private set; }

    public InvitadoService(IHttpClientFactory factory)
    {
        _factory = factory;
    }

    public async Task<List<InvitadoDto>?> GetAllAsync()
    {
        try
        {
            var client = _factory.CreateClient("ApiClient");
            var result = await client.GetFromJsonAsync<List<InvitadoDto>>(_route);
            LastError = null;
            return result;
        }
        catch
        {
            LastError = "No se pudo conectar con la API. Verifique que el backend esté ejecutándose.";
            return null;
        }
    }

    public async Task<InvitadoDto?> GetByIdAsync(int id)
    {
        try
        {
            var client = _factory.CreateClient("ApiClient");
            var result = await client.GetFromJsonAsync<InvitadoDto>($"{_route}/{id}");
            LastError = null;
            return result;
        }
        catch
        {
            LastError = "No se pudo conectar con la API. Verifique que el backend esté ejecutándose.";
            return null;
        }
    }

    public async Task<bool> CreateAsync(InvitadoDto dto)
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

    public async Task<bool> UpdateAsync(int id, InvitadoDto dto)
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
