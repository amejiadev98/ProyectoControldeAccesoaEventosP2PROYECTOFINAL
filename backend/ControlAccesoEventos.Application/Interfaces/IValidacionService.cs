using ControlAccesoEventos.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlAccesoEventos.Application.Interfaces
{
    public interface IValidacionService
    {
        Task<IEnumerable<ValidacionDto>> GetAllAsync();
        Task<ValidacionDto?> GetByIdAsync(int id);
        Task<ValidacionDto> CreateAsync(ValidacionDto dto);
        Task UpdateAsync(int id, ValidacionDto dto);
        Task DeleteAsync(int id);
    }
}
