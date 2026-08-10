using ControlAccesoEventos.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlAccesoEventos.Application.Interfaces
{
    public interface IEventoService
    {
        Task<IEnumerable<EventoDto>> GetAllAsync();
        Task<EventoDto?> GetByIdAsync(int id);
        Task<EventoDto> CreateAsync(EventoDto dto);
        Task UpdateAsync(int id, EventoDto dto);
        Task DeleteAsync(int id);
    }
}
