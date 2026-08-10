using ControlAccesoEventos.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlAccesoEventos.Application.Interfaces
{
    public interface IEntradaService
    {
        Task<IEnumerable<EntradaDto>> GetAllAsync();
        Task<EntradaDto?> GetByIdAsync(int id);
        Task<EntradaDto> CreateAsync(EntradaDto dto);
        Task UpdateAsync(int id, EntradaDto dto);
        Task DeleteAsync(int id);
    }
}
