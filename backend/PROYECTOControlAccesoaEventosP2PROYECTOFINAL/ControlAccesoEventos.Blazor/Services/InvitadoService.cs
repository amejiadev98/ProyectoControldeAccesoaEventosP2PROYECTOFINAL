using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ControlAccesoEventos.Blazor.DTOs;

namespace ControlAccesoEventos.Blazor.Services
{
    public class InvitadoService(HttpClient http)
    {
        private readonly HttpClient _http = http;

        public Task<List<InvitadoDto>?> GetAllAsync() => _http.GetFromJsonAsync<List<InvitadoDto>>("api/invitados");
        public Task<InvitadoDto?> GetByIdAsync(int id) => _http.GetFromJsonAsync<InvitadoDto>($"api/invitados/{id}");
        public Task<HttpResponseMessage> CreateAsync(InvitadoDto dto) => _http.PostAsJsonAsync("api/invitados", dto);
        public Task<HttpResponseMessage> UpdateAsync(int id, InvitadoDto dto) => _http.PutAsJsonAsync($"api/invitados/{id}", dto);
        public Task<HttpResponseMessage> DeleteAsync(int id) => _http.DeleteAsync($"api/invitados/{id}");
    }
}

