using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ControlAccesoEventos.Blazor.DTOs;

namespace ControlAccesoEventos.Blazor.Services
{
    public class EntradaService(HttpClient http)
    {
        public Task<List<EntradaDto>?> GetAllAsync() => http.GetFromJsonAsync<List<EntradaDto>>("api/entradas");
        public Task<EntradaDto?> GetByIdAsync(int id) => http.GetFromJsonAsync<EntradaDto>($"api/entradas/{id}");
        public Task<HttpResponseMessage> CreateAsync(EntradaDto dto) => http.PostAsJsonAsync("api/entradas", dto);
        public Task<HttpResponseMessage> UpdateAsync(int id, EntradaDto dto) => http.PutAsJsonAsync($"api/entradas/{id}", dto);
        public Task<HttpResponseMessage> DeleteAsync(int id) => http.DeleteAsync($"api/entradas/{id}");
    }
}

