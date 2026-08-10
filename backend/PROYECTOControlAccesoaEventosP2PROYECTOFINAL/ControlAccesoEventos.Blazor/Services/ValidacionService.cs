using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ControlAccesoEventos.Blazor.DTOs;

namespace ControlAccesoEventos.Blazor.Services
{
    public class ValidacionService(HttpClient http)
    {
        public Task<List<ValidacionDto>?> GetAllAsync() => http.GetFromJsonAsync<List<ValidacionDto>>("api/validaciones");
        public Task<ValidacionDto?> GetByIdAsync(int id) => http.GetFromJsonAsync<ValidacionDto>($"api/validaciones/{id}");
        public Task<HttpResponseMessage> CreateAsync(ValidacionDto dto) => http.PostAsJsonAsync("api/validaciones", dto);
        public Task<HttpResponseMessage> UpdateAsync(int id, ValidacionDto dto) => http.PutAsJsonAsync($"api/validaciones/{id}", dto);
        public Task<HttpResponseMessage> DeleteAsync(int id) => http.DeleteAsync($"api/validaciones/{id}");
    }
}
