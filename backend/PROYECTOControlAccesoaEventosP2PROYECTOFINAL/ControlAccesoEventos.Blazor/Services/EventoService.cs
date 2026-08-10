using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ControlAccesoEventos.Blazor.DTOs;

namespace ControlAccesoEventos.Blazor.Services
{
    public class EventoService(HttpClient http)
    {
        public Task<List<EventoDto>?> GetAllAsync() => http.GetFromJsonAsync<List<EventoDto>>("api/eventos");
        public Task<EventoDto?> GetByIdAsync(int id) => http.GetFromJsonAsync<EventoDto>($"api/eventos/{id}");
        public Task<HttpResponseMessage> CreateAsync(EventoDto dto) => http.PostAsJsonAsync("api/eventos", dto);
        public Task<HttpResponseMessage> UpdateAsync(int id, EventoDto dto) => http.PutAsJsonAsync($"api/eventos/{id}", dto);
        public Task<HttpResponseMessage> DeleteAsync(int id) => http.DeleteAsync($"api/eventos/{id}");
    }
}

